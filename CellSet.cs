namespace SudokuSolver
{
    class CellSet
    {
        private readonly List<Cell[]> _cellSets; 

        public List<Cell[]> CellSets => _cellSets;

        public CellSet()
        {
            _cellSets = new List<Cell[]>();
        }

        public bool IsCellSetSolved()
        {
            var count = _cellSets.Count;

            for (int i = 0; i < count; ++i)
            {
                var cellSet = _cellSets[i];
                var setCount = cellSet.Length;

                for (int j = 0; j < setCount; ++j)
                    if (!cellSet[j].IsSolved)
                        return false;
            }

            return true;
        }
    }
}