
using StarGrabber.Backgrounds.Interfaces;

namespace StarGrabber.Backgrounds
{
    public class HyperSpace : ISpace
    {
        public char starSymbol => '|';

        public byte starDensity => 255;

        public byte idleSpeed => 255;

        public bool paralax => false;
    }
}
