using Bingo.Generators;

namespace Bingo.Tests
{
    public class BingoStripGeneratorTests
    {
        [Fact]
        public void TestStripGeneration_ShouldGenerate6Cards()
        {
            var strip = BingoStripGenerator.GenerateStrip();
            
            Assert.NotNull(strip);
            Assert.Equal(6, strip.Length);
        }
        
        [Fact]
        public void TestStripGeneration_EachCardHas15Numbers()
        {
            var strip = BingoStripGenerator.GenerateStrip();
            
            foreach (var card in strip)
            {
                var numbers = card.GetAllNumbers().ToList();
                Assert.Equal(15, numbers.Count);
            }
        }
        
        [Fact]
        public void TestStripGeneration_EachCardHas5NumbersPerRow()
        {
            var strip = BingoStripGenerator.GenerateStrip();
            
            foreach (var card in strip)
            {
                for (int row = 0; row < 3; row++)
                {
                    int count = 0;
                    for (int col = 0; col < 9; col++)
                    {
                        if (card[row, col] != 0)
                            count++;
                    }
                    Assert.Equal(5, count);
                }
            }
        }
        
        [Fact]
        public void TestStripGeneration_EachCardHasCorrectColumnRanges()
        {
            var strip = BingoStripGenerator.GenerateStrip();
            
            foreach (var card in strip)
            {
                for (int col = 0; col < 9; col++)
                {
                    int minExpected = (col == 0) ? 1 : col * 10;
                    int maxExpected = (col == 8) ? 90 : (col + 1) * 10 - 1;
                    
                    for (int row = 0; row < 3; row++)
                    {
                        int number = card[row, col];
                        if (number != 0)
                        {
                            Assert.InRange(number, minExpected, maxExpected);
                        }
                    }
                }
            }
        }
        
        [Fact]
        public void TestStripGeneration_NumbersAreSortedWithinColumnsOnEachCard()
        {
            var strip = BingoStripGenerator.GenerateStrip();
            
            foreach (var card in strip)
            {
                for (int col = 0; col < 9; col++)
                {
                    var columnNumbers = new List<int>();
                    for (int row = 0; row < 3; row++)
                    {
                        if (card[row, col] != 0)
                            columnNumbers.Add(card[row, col]);
                    }
                    
                    // Numbers should be in ascending order
                    var sortedNumbers = columnNumbers.OrderBy(n => n).ToList();
                    Assert.Equal(sortedNumbers, columnNumbers);
                }
            }
        }
        
        [Fact]
        public void TestStripGeneration_CardsHaveSequentialSerialNumbers()
        {
            Bingo.BingoCard.ResetSerialNumberCounter();
            
            var strip = BingoStripGenerator.GenerateStrip();
            
            for (int i = 0; i < 6; i++)
            {
                Assert.Equal(i + 1, strip[i].SerialNumber);
            }
        }
        
        [Fact]
        public void TestStripGeneration_DisplayAllCards()
        {
            var strip = BingoStripGenerator.GenerateStrip();
            
            Console.WriteLine("========== COMPLETE BINGO STRIP (6 CARDS) ==========");
            foreach (var card in strip)
            {
                Console.WriteLine(card.ToString());
            }
            
            // Verify we can generate HTML for the entire strip
            var html = Bingo.BingoCard.ToHtmlDocument(strip);
            Assert.NotEmpty(html);
            Assert.Contains("BINGO CARD", html);
        }
        
        [Fact]
        public void TestStripGeneration_EachCardIsValid()
        {
            var strip = BingoStripGenerator.GenerateStrip();
            
            // Each card should be a valid bingo card
            foreach (var card in strip)
            {
                // 15 numbers total
                Assert.Equal(15, card.GetAllNumbers().Count());
                
                // 5 numbers per row
                for (int row = 0; row < 3; row++)
                {
                    int rowCount = 0;
                    for (int col = 0; col < 9; col++)
                    {
                        if (card[row, col] != 0)
                            rowCount++;
                    }
                    Assert.Equal(5, rowCount);
                }
                
                // Each column has 1-3 numbers
                for (int col = 0; col < 9; col++)
                {
                    int colCount = 0;
                    for (int row = 0; row < 3; row++)
                    {
                        if (card[row, col] != 0)
                            colCount++;
                    }
                    Assert.InRange(colCount, 1, 3);
                }
            }
        }
        
        [Fact]
        public void TestMultipleStrips_AreUnique()
        {
            Bingo.BingoCard.ResetSerialNumberCounter();
            
            var strip1 = BingoStripGenerator.GenerateStrip();
            var strip2 = BingoStripGenerator.GenerateStrip();
            
            // At least some cards should be different
            bool foundDifference = false;
            for (int cardIndex = 0; cardIndex < 6 && !foundDifference; cardIndex++)
            {
                var card1Numbers = strip1[cardIndex].GetAllNumbers().ToList();
                var card2Numbers = strip2[cardIndex].GetAllNumbers().ToList();
                
                if (!card1Numbers.SequenceEqual(card2Numbers))
                {
                    foundDifference = true;
                }
            }
            
            Assert.True(foundDifference, "Two strips should have at least some different cards");
        }
    }
}
