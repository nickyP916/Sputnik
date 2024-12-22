namespace Sputnik
{
    public interface IMiniGame
    {
        string Name { get; }

        Task Play(CancellationToken token);
    }
}
