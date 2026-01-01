namespace Bingo.Generators
{
    public interface IBingoCardGenerator
    {
        static abstract IEnumerable<int> GenerateCard();
    }

    public class BingoCardGenerator : IBingoCardGenerator
    {
        public static IEnumerable<int> GenerateCard()
        {
            var random = new Random();
            var calledNumbers = new HashSet<int>();
            do
            {
                var r = random.Next(1, 91);
                if (calledNumbers.Add(r))
                    yield return r;
            } while (calledNumbers.Count <= 90);
        }
    }
}
