namespace Stage0
{
    partial class Program
    {
        private static void Main(string[] args)
        {
            Welcome6831();
            Welcome2813();
            Console.ReadKey();
        }
        static partial void Welcome6831();
        private static void Welcome2813()
        {
            Console.WriteLine("Enter your name: ");
            string? name = Console.ReadLine();
            Console.WriteLine("{0},welome to my first console application", name);
        }
    }
}