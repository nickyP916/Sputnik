namespace Sputnik
{
    static internal class Game
    {
        public static List<IMiniGame> MiniGames { get; set; }

        public static async Task Play()
        {
            var _numberCrunch = new NumberCrunch();
            await _numberCrunch.Play();
        }
    }
}
