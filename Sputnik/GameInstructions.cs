namespace Sputnik;

public record GameInstructions
{
    public required Func<object,string?,Task<bool>> ScoreLogic { get; set; }
    public required Func<object> Setup { get; set; }
}