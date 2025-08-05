using StarGrabber.IO.Interfaces;

namespace StarGrabber.IO
{
    public class ConsoleReader : IReader
    {
        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
