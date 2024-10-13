namespace Sputnik
{
    public interface IMiniGame
    {
        string Name { get; }

        public Task Play(CancellationToken token);

    }
}
