namespace SudokuSolver
{
    class Program
    {
        public static void Main(string[] args)
        {
            var inputFileName = "expert2";

            Solver solver = new Solver(inputFileName);

            solver.Solve();
        }
    }
}