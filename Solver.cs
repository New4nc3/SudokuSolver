using InputOutput;
using System.Text;

namespace SudokuSolver
{
    class Solver
    {
        private const int RowsColsCount = 9;

        private readonly string _inputFileName;
        private readonly string _outputFileName;
        
        private readonly string[] _rawData;

        private readonly List<Cell[]> _rows;
        private readonly List<Cell[]> _cols;
        private readonly List<Cell[]> _grids3x3;
        private readonly Cell[,] _fullGrid;

        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public Solver(string inputFileName, string outputFileName)
        {
            _inputFileName = inputFileName;
            _outputFileName = outputFileName;

            _rows = new List<Cell[]>(RowsColsCount);
            _cols = new List<Cell[]>(RowsColsCount);
            _grids3x3 = new List<Cell[]>(RowsColsCount);
            _fullGrid = new Cell[RowsColsCount, RowsColsCount];

            _rawData = FileManager.ReadDataFromFile(_inputFileName);

            Initialize();
        }

        private void Initialize()
        {
            InitFullGrid();
            InitRowsAndCols();
            Init3x3Grids();
        }

        private void InitFullGrid()
        {
            for (int i = 0; i < RowsColsCount; ++i)
            {
                var row = _rawData[i].Split('\t');

                for (int j = 0; j < RowsColsCount; ++j)
                    _fullGrid[i, j] = new Cell(byte.Parse(row[j]));
            }
        }

        private void InitRowsAndCols()
        {
            for (int i = 0; i < RowsColsCount; ++i)
            {
                var tempRow = new Cell[RowsColsCount];
                var tempCol = new Cell[RowsColsCount];

                for (int j = 0; j < RowsColsCount; ++j)
                {
                    tempRow[j] = _fullGrid[i, j];
                    tempCol[j] = _fullGrid[j, i];
                }

                _rows.Add(tempRow);
                _cols.Add(tempCol);
            }
        }

        private void Init3x3Grids()
        {
            var rowsInGridCount = RowsColsCount / 3;
            var colsInGridCount = RowsColsCount / 3;

            for (int k = 0; k < RowsColsCount; ++k)
            {
                var counter = 0;
                var tempGrid = new Cell[RowsColsCount];

                for (int i = 0; i < rowsInGridCount; ++i)
                    for (int j = 0; j < colsInGridCount; ++j)
                        tempGrid[counter++] = _fullGrid[(k / rowsInGridCount) * rowsInGridCount + i, (k % colsInGridCount) * colsInGridCount + j];

                _grids3x3.Add(tempGrid);
            }
        }

        public void Solve()
        {
            int iterationsCount = 0;

            do
            {
                Cell.ResetCellChanged();

                _rows.ForEach(x => CheckAndRemoveCandidates(x));
                _cols.ForEach(x => CheckAndRemoveCandidates(x));
                _grids3x3.ForEach(x => 
                { 
                    CheckAndRemoveCandidates(x);
                    CheckForUniqueCandidate(x);
                });

                ++iterationsCount;

                Console.WriteLine(this);
            }
            while (Cell.CellChanged);

            var unresolvedCellsCount = GetUnresolvedCount();
            if (unresolvedCellsCount > 0)
            {
                Console.WriteLine($"Finished. Unresolved: {unresolvedCellsCount}. Iterations count: {iterationsCount}");
            }
            else
            {
                Console.WriteLine($"Sucessfully solved in {iterationsCount} moves!");
            }

            if (FileManager.WriteDataToFile(_outputFileName, this.ToString()))
            {
                Console.WriteLine($"Successfully saved to \"{_outputFileName}\"");
            }

            Console.WriteLine("\nPress any key to exit . . .");
            Console.ReadKey(true);
        }

        private void CheckAndRemoveCandidates(Cell[] cellsToCheck)
        {
            var nonSolvedCells = cellsToCheck.Where(x => !x.IsSolved).ToList();
            if (nonSolvedCells.Count == 0)
                return;

            var candidatesToRemove = cellsToCheck.Except(nonSolvedCells).Select(x => x.Value).ToList();

            foreach (var nonSolved in nonSolvedCells)
                nonSolved.RemoveCandidates(candidatesToRemove);
        }

        private void CheckForUniqueCandidate(Cell[] cellsToCheck)
        {
            if (cellsToCheck.All(x => x.IsSolved))
                return;

            var unresolvedCells = cellsToCheck.Where(x => !x.IsSolved);

            var allUniqueCandidates = unresolvedCells.SelectMany(x => x.GetCandidates, (x, candidates) => new { x, candidates })
                .GroupBy(x => x.candidates)
                .Select(group => new { Key = group.Key, Count = group.Count() })
                .Where(x => x.Count == 1)
                .ToList();

            foreach (var uniqueCandidate in allUniqueCandidates)
            {
                var valueToSet = uniqueCandidate.Key;
                var cellToSet = unresolvedCells.First(x => x.GetCandidates.Contains(valueToSet));
                cellToSet.SetUniqueCandidate(valueToSet);
            }
        }

        private int GetUnresolvedCount() =>
            _rows.Sum(x => x.Count(x => !x.IsSolved));

        public override string ToString()
        {
            _stringBuilder.Clear();

            for (int i = 0; i < RowsColsCount; ++i)
            {
                if (i % 3 == 0 && i != 0)
                    _stringBuilder.Append('\n');

                for (int j = 0; j < RowsColsCount; ++j)
                {
                    if (j != 0 && j % 3 == 0)
                        _stringBuilder.Append(' ');

                    _stringBuilder.Append(_fullGrid[i, j]);
                }

                _stringBuilder.Append('\n');
            }

            return _stringBuilder.ToString();
        }
    }
}