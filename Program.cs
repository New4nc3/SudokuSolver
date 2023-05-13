using InputOutput;

namespace SudokuSolver
{
    class Program
    {
        public static void Main(string[] args)
        {
            var input = "hard2.txt";
            var output = "hard2_out.txt";

            Solver solver = new Solver(input.GenerateInputFilePath(), output.GenerateOutputFilePath());
            solver.Solve();
        }
    }
}