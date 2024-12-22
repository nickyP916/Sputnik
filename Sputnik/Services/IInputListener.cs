namespace Sputnik.Services
{
    public interface IInputListener
    {
        public string? ListenForInput(CancellationToken token);
    }
}
