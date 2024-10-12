namespace Sputnik
{
    static internal class Game
    {
        public static List<IMiniGame> MiniGames { get; set; }

        public static void Play()
        {
            var _numberCrunch = new NumberCrunch();
            _numberCrunch.Play();
        }
    }
}
