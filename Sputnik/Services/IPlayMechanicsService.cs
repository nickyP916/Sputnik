
namespace Sputnik.Services
{
    public interface IPlayMechanicsService
    {
        Task<int> PlayToMaxRounds(GameInstructions gameInstructions, CancellationToken token);
    }
}