namespace Bingo.Generators
{
    /// <summary>
    /// Generates a Bingo "strip" - a set of 6 bingo cards.
    /// Each card is independently generated following standard 90-ball bingo rules.
    /// Note: Numbers may repeat across the 6 cards (not a true UK bingo strip).
    /// For a true strip where all 90 numbers appear exactly once, a more complex algorithm is needed.
    /// </summary>
    public class BingoStripGenerator
    {
        /// <summary>
        /// Generates a strip of 6 bingo cards.
        /// </summary>
        public static BingoCard[] GenerateStrip()
        {
            var cards = new BingoCard[6];
            
            for (int i = 0; i < 6; i++)
            {
                cards[i] = BingoCardGenerator.GenerateCard();
            }
            
            return cards;
        }
    }
}
