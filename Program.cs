namespace SudokuSolver
{
    class Program
    {
        public static void Main(string[] args)
        {
            var inputFileName = "hard1";

            Solver solver = new Solver(inputFileName);

            solver.Solve();
        }
    }
}