namespace Bingo
{
    public interface IBingoNumberGenerator
    {
        int GetNextNumber();
    }

    public class BingoNumberGenerator : IBingoNumberGenerator
    {
        private readonly Random _random;
        private readonly HashSet<int> _generatedNumbers;

        public BingoNumberGenerator()
        {
            _random = new Random();
            _generatedNumbers = [];
        }

        public int GetNextNumber()
        {
            int nextNumber;
            do
            {
                nextNumber = _random.Next(1, 90);
            } while (_generatedNumbers.Contains(nextNumber));

            _generatedNumbers.Add(nextNumber);
            return nextNumber;
        }
    }
}
