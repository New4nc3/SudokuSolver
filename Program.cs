namespace SudokuSolver
{
    class Program
    {
        public static void Main(string[] args)
        {
            Solver solver = new Solver("hard2.txt", "hard2_out.txt");
            solver.Solve();
        }
    }
}