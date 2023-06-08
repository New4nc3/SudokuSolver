namespace SudokuSolver
{
    class Program
    {
        public static void Main(string[] args)
        {
            var inputFileName = "expert3";

            Solver solver = new Solver(inputFileName);
            solver.Solve();

            Console.WriteLine("\nPress any key to exit . . .");
            Console.ReadKey(true);
        }
    }
}