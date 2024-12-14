using Draw;

namespace Sputnik
{
    internal class PatternMatch : IMiniGame
    {
        public string Name => "Pattern Match";

        public async Task Play(CancellationToken token)
        {
            Console.WriteLine($"You are playing {Name}!");

            ShapeDrawer.DrawShapeSequence(3, 4);
        }
    }
}
