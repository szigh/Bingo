namespace Bingo
{
    public interface IBingoNumberGenerator
    {
        bool TryGetNextNumber(out int nextNumber);
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

        public bool TryGetNextNumber(out int nextNumber)
        {
            if (_generatedNumbers.Count >= 89)
            {
                nextNumber = -1;
                return false; // All numbers have been generated
            }
            do
            {
                nextNumber = _random.Next(1, 90);
            } while (_generatedNumbers.Contains(nextNumber));

            _generatedNumbers.Add(nextNumber);
            return true;
        }
    }
}
