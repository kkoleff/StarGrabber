
using StarGrabber.Backgrounds.Interfaces;

namespace StarGrabber.Backgrounds
{
    public class OrbitalSpace : ISpace
    {
        public char starSymbol => '.';

        public byte starDensity => 240;

        public byte idleSpeed => 0;

        public bool paralax => false;
    }
}
