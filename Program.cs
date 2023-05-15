namespace SudokuSolver
{
    class Program
    {
        public static void Main(string[] args)
        {
            var inputFileName = "expert3";

            Solver solver = new Solver(inputFileName);

            solver.Solve();
        }
    }
}