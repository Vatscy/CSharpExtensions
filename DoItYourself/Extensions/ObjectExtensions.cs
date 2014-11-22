
namespace System
{
    public static class ObjectExtensions
    {
        public static void ConsoleWriteLine(this object x)
        {
            Console.WriteLine(x);
        }

        public static void ConsoleWriteLine(this object x, string format)
        {
            Console.WriteLine(format, x);
        }
    }
}
