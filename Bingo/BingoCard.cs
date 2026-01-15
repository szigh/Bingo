using System;
using System.Collections.Generic;
using System.Text;

namespace Bingo
{
    /// <summary>
    /// Represents a 90-ball Bingo card with a unique serial number.
    /// The card contains a 3x9 grid with 15 numbers and 12 empty spaces.
    /// </summary>
    public record BingoCard
    {
        private static int _nextSerialNumber = 1;
        private readonly int[,] _grid;

        public BingoCard(int[,] grid)
        {
            if (grid.GetLength(0) != 3 || grid.GetLength(1) != 9)
                throw new ArgumentException("Bingo card must be a 3x9 grid", nameof(grid));

            _grid = grid;
            SerialNumber = _nextSerialNumber++;
        }

        public int SerialNumber { get; }

        /// <summary>
        /// Gets the number at the specified row and column.
        /// Returns 0 for empty cells.
        /// </summary>
        public int this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= 3 || col < 0 || col >= 9)
                    throw new ArgumentOutOfRangeException($"Invalid position: [{row},{col}]");
                return _grid[row, col];
            }
        }

        /// <summary>
        /// Gets all numbers on the card (excluding empty spaces).
        /// </summary>
        public IEnumerable<int> GetAllNumbers()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (_grid[row, col] != 0)
                        yield return _grid[row, col];
                }
            }
        }

        /// <summary>
        /// Checks if the card contains a specific number.
        /// </summary>
        public bool ContainsNumber(int number)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (_grid[row, col] == number)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns a formatted string representation of the card.
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"═══════════════════════════════════════════");
            sb.AppendLine($"  BINGO CARD #{SerialNumber:D6}");
            sb.AppendLine($"═══════════════════════════════════════════");

            for (int row = 0; row < 3; row++)
            {
                sb.Append("║ ");
                for (int col = 0; col < 9; col++)
                {
                    if (_grid[row, col] == 0)
                        sb.Append("   ");
                    else
                        sb.Append($"{_grid[row, col],2} ");

                    sb.Append(col < 8 ? "│ " : "");
                }
                sb.AppendLine(" ║");

                if (row < 2)
                    sb.AppendLine("║───┼───┼───┼───┼───┼───┼───┼───┼───║");
            }

            sb.AppendLine("═══════════════════════════════════════════");
            return sb.ToString();
        }

        /// <summary>
        /// Returns a compact string representation (single line per row).
        /// </summary>
        public string ToCompactString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Card #{SerialNumber}:");
            
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (_grid[row, col] == 0)
                        sb.Append("   ");
                    else
                        sb.Append($"{_grid[row, col],3}");
                }
                sb.AppendLine();
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// Returns an HTML representation of the card, optimized for printing.
        /// </summary>
        public string ToHtml()
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("<div class=\"bingo-card\">");
            sb.AppendLine($"  <div class=\"card-header\">BINGO CARD #{SerialNumber:D6}</div>");
            sb.AppendLine("  <table class=\"bingo-grid\">");
            
            for (int row = 0; row < 3; row++)
            {
                sb.AppendLine("    <tr>");
                for (int col = 0; col < 9; col++)
                {
                    if (_grid[row, col] == 0)
                        sb.AppendLine("      <td class=\"empty\"></td>");
                    else
                        sb.AppendLine($"      <td class=\"number\">{_grid[row, col]}</td>");
                }
                sb.AppendLine("    </tr>");
            }
            
            sb.AppendLine("  </table>");
            sb.AppendLine("</div>");
            
            return sb.ToString();
        }

        /// <summary>
        /// Generates a complete HTML document with embedded CSS for one or more bingo cards.
        /// Perfect for printing or sharing via email/web.
        /// </summary>
        public static string ToHtmlDocument(params BingoCard[] cards)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("  <meta charset=\"UTF-8\">");
            sb.AppendLine("  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            sb.AppendLine("  <title>Bingo Cards</title>");
            sb.AppendLine("  <style>");
            sb.AppendLine("    @media print {");
            sb.AppendLine("      .bingo-card {");
            sb.AppendLine("        page-break-after: always;");
            sb.AppendLine("        page-break-inside: avoid;");
            sb.AppendLine("      }");
            sb.AppendLine("      @page {");
            sb.AppendLine("        margin: 1cm;");
            sb.AppendLine("      }");
            sb.AppendLine("    }");
            sb.AppendLine("    body {");
            sb.AppendLine("      font-family: Arial, sans-serif;");
            sb.AppendLine("      margin: 20px;");
            sb.AppendLine("      background-color: #f5f5f5;");
            sb.AppendLine("    }");
            sb.AppendLine("    .bingo-card {");
            sb.AppendLine("      background: white;");
            sb.AppendLine("      border: 3px solid #333;");
            sb.AppendLine("      border-radius: 10px;");
            sb.AppendLine("      padding: 20px;");
            sb.AppendLine("      margin: 20px auto;");
            sb.AppendLine("      max-width: 800px;");
            sb.AppendLine("      box-shadow: 0 4px 6px rgba(0,0,0,0.1);");
            sb.AppendLine("    }");
            sb.AppendLine("    .card-header {");
            sb.AppendLine("      text-align: center;");
            sb.AppendLine("      font-size: 24px;");
            sb.AppendLine("      font-weight: bold;");
            sb.AppendLine("      color: #333;");
            sb.AppendLine("      margin-bottom: 15px;");
            sb.AppendLine("      padding: 10px;");
            sb.AppendLine("      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);");
            sb.AppendLine("      color: white;");
            sb.AppendLine("      border-radius: 5px;");
            sb.AppendLine("    }");
            sb.AppendLine("    .bingo-grid {");
            sb.AppendLine("      width: 100%;");
            sb.AppendLine("      border-collapse: collapse;");
            sb.AppendLine("      margin: 0 auto;");
            sb.AppendLine("    }");
            sb.AppendLine("    .bingo-grid td {");
            sb.AppendLine("      width: 11.11%;");
            sb.AppendLine("      height: 60px;");
            sb.AppendLine("      text-align: center;");
            sb.AppendLine("      vertical-align: middle;");
            sb.AppendLine("      font-size: 24px;");
            sb.AppendLine("      font-weight: bold;");
            sb.AppendLine("      border: 2px solid #333;");
            sb.AppendLine("    }");
            sb.AppendLine("    .bingo-grid td.number {");
            sb.AppendLine("      background-color: #fff;");
            sb.AppendLine("      color: #333;");
            sb.AppendLine("    }");
            sb.AppendLine("    .bingo-grid td.empty {");
            sb.AppendLine("      background: repeating-linear-gradient(");
            sb.AppendLine("        45deg,");
            sb.AppendLine("        #f0f0f0,");
            sb.AppendLine("        #f0f0f0 10px,");
            sb.AppendLine("        #e0e0e0 10px,");
            sb.AppendLine("        #e0e0e0 20px");
            sb.AppendLine("      );");
            sb.AppendLine("    }");
            sb.AppendLine("    .bingo-grid td.number:hover {");
            sb.AppendLine("      background-color: #fffacd;");
            sb.AppendLine("      cursor: pointer;");
            sb.AppendLine("    }");
            sb.AppendLine("  </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            
            foreach (var card in cards)
            {
                sb.AppendLine(card.ToHtml());
            }
            
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            
            return sb.ToString();
        }

        public static void ResetSerialNumberCounter()
        {
            _nextSerialNumber = 1;
        }
    }
}
