namespace Bingo.Generators
{
    public interface IBingoNumberGenerator
    {
        bool TryGetNextNumber(out int nextNumber);
        void Reset();
    }

    public class BingoNumberGenerator : IBingoNumberGenerator
    {
        private readonly Random _random;
        private readonly HashSet<int> _generatedNumbers;
        private readonly object _lock = new();

        public BingoNumberGenerator()
        {
            _random = new Random();
            _generatedNumbers = new HashSet<int>();
        }

        public bool TryGetNextNumber(out int nextNumber)
        {
            lock (_lock)
            {
                if (_generatedNumbers.Count >= 90)
                {
                    nextNumber = -1;
                    return false; // All numbers have been generated
                }
                do
                {
                    nextNumber = _random.Next(1, 91);
                } while (_generatedNumbers.Contains(nextNumber));

                _generatedNumbers.Add(nextNumber);
                return true;
            }
        }

        public void Reset()
        {
            lock (_lock)
            {
                _generatedNumbers.Clear();
            }
        }
    }
}
