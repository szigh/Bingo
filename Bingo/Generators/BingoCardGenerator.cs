namespace Bingo.Generators
{
    public interface IBingoCardGenerator
    {
        static abstract BingoCard GenerateCard();
    }

    /// <summary>
    /// Generates 90-ball Bingo cards following UK Bingo rules:
    /// - 3 rows × 9 columns grid (27 cells)
    /// - 15 numbers total (12 empty spaces)
    /// - Each row has exactly 5 numbers and 4 empty spaces
    /// - Each column has 1, 2, or 3 numbers (at least 1)
    /// - Column 0: 1-9, Column 1: 10-19, ..., Column 8: 80-90
    /// - Numbers within columns are sorted in ascending order
    /// </summary>
    public class BingoCardGenerator : IBingoCardGenerator
    {
        public static BingoCard GenerateCard()
        {
            var random = new Random();
            var grid = new int[3, 9];
            
            // Step 1: Decide how many numbers go in each column (must total 15)
            var numbersPerColumn = AllocateNumbersToColumns(random);
            
            // Step 2: For each column, decide which rows get numbers
            var columnRowPlacements = new List<int>[9];
            for (int col = 0; col < 9; col++)
            {
                columnRowPlacements[col] = ChooseRowsForColumn(numbersPerColumn[col], random);
            }
            
            // Step 3: Balance the grid so each row has exactly 5 numbers
            BalanceRowCounts(numbersPerColumn, columnRowPlacements, random);
            
            // Step 4: Fill each column with actual numbers from the correct range
            for (int col = 0; col < 9; col++)
            {
                PopulateColumnWithNumbers(grid, col, columnRowPlacements[col], random);
            }
            
            return new BingoCard(grid);
        }

        /// <summary>
        /// Allocates 15 numbers across 9 columns, ensuring each column gets 1-3 numbers.
        /// </summary>
        private static int[] AllocateNumbersToColumns(Random random)
        {
            var allocation = new int[9];
            
            // Start with 1 number per column (9 numbers allocated)
            for (int i = 0; i < 9; i++)
            {
                allocation[i] = 1;
            }
            
            // Distribute the remaining 6 numbers randomly
            int remaining = 6;
            while (remaining > 0)
            {
                int col = random.Next(9);
                if (allocation[col] < 3) // Max 3 numbers per column
                {
                    allocation[col]++;
                    remaining--;
                }
            }
            
            return allocation;
        }

        /// <summary>
        /// Randomly selects which rows (0, 1, or 2) will contain numbers for a given column.
        /// </summary>
        private static List<int> ChooseRowsForColumn(int count, Random random)
        {
            var rows = new List<int> { 0, 1, 2 };
            var selected = new List<int>();
            
            for (int i = 0; i < count; i++)
            {
                int index = random.Next(rows.Count);
                selected.Add(rows[index]);
                rows.RemoveAt(index);
            }
            
            selected.Sort();
            return selected;
        }

        /// <summary>
        /// Adjusts the row placements so that each row has exactly 5 numbers.
        /// </summary>
        private static void BalanceRowCounts(int[] numbersPerColumn, List<int>[] columnRowPlacements, Random random)
        {
            const int targetNumbersPerRow = 5;
            var numbersPerRow = new int[3];
            
            // Count current numbers in each row
            for (int col = 0; col < 9; col++)
            {
                foreach (var row in columnRowPlacements[col])
                {
                    numbersPerRow[row]++;
                }
            }
            
            // Iteratively adjust until all rows have exactly 5 numbers
            int attempts = 0;
            const int maxAttempts = 100;
            
            while (attempts++ < maxAttempts)
            {
                bool isBalanced = true;
                
                for (int row = 0; row < 3; row++)
                {
                    if (numbersPerRow[row] < targetNumbersPerRow)
                    {
                        isBalanced = false;
                        AddNumberToRow(row, numbersPerColumn, columnRowPlacements, numbersPerRow, random);
                    }
                    else if (numbersPerRow[row] > targetNumbersPerRow)
                    {
                        isBalanced = false;
                        RemoveNumberFromRow(row, numbersPerColumn, columnRowPlacements, numbersPerRow, random);
                    }
                }
                
                if (isBalanced)
                    break;
            }
        }

        /// <summary>
        /// Adds a number to a specific row by finding a suitable column.
        /// </summary>
        private static void AddNumberToRow(int targetRow, int[] numbersPerColumn, List<int>[] columnRowPlacements, int[] numbersPerRow, Random random)
        {
            var eligibleColumns = new List<int>();
            
            for (int col = 0; col < 9; col++)
            {
                // Column must not already have this row AND can accept another number
                if (!columnRowPlacements[col].Contains(targetRow) && numbersPerColumn[col] < 3)
                {
                    eligibleColumns.Add(col);
                }
            }
            
            if (eligibleColumns.Count > 0)
            {
                int selectedCol = eligibleColumns[random.Next(eligibleColumns.Count)];
                columnRowPlacements[selectedCol].Add(targetRow);
                columnRowPlacements[selectedCol].Sort();
                numbersPerColumn[selectedCol]++;
                numbersPerRow[targetRow]++;
            }
        }

        /// <summary>
        /// Removes a number from a specific row by finding a suitable column.
        /// </summary>
        private static void RemoveNumberFromRow(int targetRow, int[] numbersPerColumn, List<int>[] columnRowPlacements, int[] numbersPerRow, Random random)
        {
            var eligibleColumns = new List<int>();
            
            for (int col = 0; col < 9; col++)
            {
                // Column must have this row AND can afford to lose a number (keep at least 1)
                if (columnRowPlacements[col].Contains(targetRow) && numbersPerColumn[col] > 1)
                {
                    eligibleColumns.Add(col);
                }
            }
            
            if (eligibleColumns.Count > 0)
            {
                int selectedCol = eligibleColumns[random.Next(eligibleColumns.Count)];
                columnRowPlacements[selectedCol].Remove(targetRow);
                numbersPerColumn[selectedCol]--;
                numbersPerRow[targetRow]--;
            }
        }

        /// <summary>
        /// Fills a column with random numbers from the appropriate range, sorted in ascending order.
        /// </summary>
        private static void PopulateColumnWithNumbers(int[,] grid, int col, List<int> rows, Random random)
        {
            // Determine the number range for this column
            int minNumber = (col == 0) ? 1 : col * 10;
            int maxNumber = (col == 8) ? 90 : (col + 1) * 10 - 1;
            
            // Create a pool of available numbers
            var availableNumbers = Enumerable.Range(minNumber, maxNumber - minNumber + 1).ToList();
            
            // Randomly select the required count of numbers
            var selectedNumbers = new List<int>();
            for (int i = 0; i < rows.Count; i++)
            {
                int index = random.Next(availableNumbers.Count);
                selectedNumbers.Add(availableNumbers[index]);
                availableNumbers.RemoveAt(index);
            }
            
            // Sort the numbers (traditional bingo cards have sorted columns)
            selectedNumbers.Sort();
            
            // Place the numbers in the card at the specified rows
            for (int i = 0; i < rows.Count; i++)
            {
                grid[rows[i], col] = selectedNumbers[i];
            }
        }
    }
}
