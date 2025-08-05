using StarGrabber.Backgrounds.Interfaces;


namespace StarGrabber.Backgrounds
{
    public class StarSystemSpace : ISpace
    {
        public char starSymbol => '.';

        public byte starDensity => 240;

        public byte idleSpeed => 216;

        public bool paralax => false;
    }
}
