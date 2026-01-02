using Bingo.Generators;

namespace Bingo.Tests
{
    public class BingoCardGenerator
    {
        [Fact]
        public void TestBingoCardGeneration()
        {
            var card = Generators.BingoCardGenerator.GenerateCard();

            // Display the card using ToString
            Console.WriteLine(card.ToString());
            
            // Verify constraints
            Assert.NotNull(card);
            Assert.True(card.SerialNumber > 0);
            
            // Verify 15 numbers on the card
            var numbers = card.GetAllNumbers().ToList();
            Assert.Equal(15, numbers.Count);
            
            // Verify each row has exactly 5 numbers
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
            
            // Verify each column has at least 1 number
            for (int col = 0; col < 9; col++)
            {
                int count = 0;
                for (int row = 0; row < 3; row++)
                {
                    if (card[row, col] != 0)
                        count++;
                }
                Assert.InRange(count, 1, 3);
            }
            
            // Verify numbers are in correct column ranges
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
        
        [Fact]
        public void TestMultipleCardsHaveUniqueSerialNumbers()
        {
            Bingo.BingoCard.ResetSerialNumberCounter();
            
            var card1 = Generators.BingoCardGenerator.GenerateCard();
            var card2 = Generators.BingoCardGenerator.GenerateCard();
            var card3 = Generators.BingoCardGenerator.GenerateCard();
            
            Assert.Equal(1, card1.SerialNumber);
            Assert.Equal(2, card2.SerialNumber);
            Assert.Equal(3, card3.SerialNumber);
        }
        
        [Fact]
        public void TestCardContainsNumber()
        {
            var card = Generators.BingoCardGenerator.GenerateCard();
            var numbers = card.GetAllNumbers().ToList();
            
            // Card should contain all its numbers
            foreach (var number in numbers)
            {
                Assert.True(card.ContainsNumber(number));
            }
            
            // Card should not contain number 0
            Assert.False(card.ContainsNumber(0));
        }
    }
}