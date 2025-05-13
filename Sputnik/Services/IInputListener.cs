namespace Sputnik.Services
{
    public interface IInputListener
    {
        string? ListenForInput(CancellationToken token);
    }
}
