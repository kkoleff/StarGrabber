using StarGrabber.Backgrounds.Interfaces;

namespace StarGrabber.Backgrounds
{
    public class InterGalaxySpace : ISpace
    {
        public char starSymbol => '.';

        public byte starDensity => 128;

        public byte idleSpeed => 64;

        public bool paralax => true;
    }
}
