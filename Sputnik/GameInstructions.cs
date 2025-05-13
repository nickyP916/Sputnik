using Sputnik.Services;

namespace Sputnik;

public record GameInstructions
{
    public required IInputListener InputListener { get; set; }
    public required Func<object,string?,Task<bool>> ScoreLogic { get; set; }
    public required Func<object> Setup { get; set; }
}