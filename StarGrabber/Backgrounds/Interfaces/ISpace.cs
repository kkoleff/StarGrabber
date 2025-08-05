
namespace StarGrabber.Backgrounds.Interfaces
{
    public interface ISpace
    {
        char starSymbol { get; }

        byte starDensity { get; }

        byte idleSpeed { get; }

        bool paralax { get; }
    }
}
