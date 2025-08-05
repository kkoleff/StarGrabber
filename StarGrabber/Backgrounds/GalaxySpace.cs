
using StarGrabber.Backgrounds.Interfaces;

namespace StarGrabber.Backgrounds
{
    public class GalaxySpace : ISpace
    {
        public char starSymbol => '.';

        public byte starDensity => 216;

        public byte idleSpeed => 128;

        public bool paralax => true;
    }
}
