using Sputnik.Services;

namespace Sputnik;

public record GameInstructions
{
    public required IInputListener InputListener { get; set; }
    public required Func<object,object,bool> ScoreLogic { get; set; }
    public required Func<Tuple<int,int>> Setup { get; set; }
}