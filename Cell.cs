using System.Text;

namespace SudokuSolver
{
    class Cell
    {
        public static bool CellChanged { get; private set; }

        private static readonly HashSet<byte> _allCandidates = new HashSet<byte> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private readonly HashSet<byte> _candidates = new HashSet<byte>();

        public byte Value { get; private set; }
        public bool IsSolved => Value != 0;
        public IEnumerable<byte> GetCandidates => _candidates;

        public Cell(byte value)
        {
            Value = value;

            if (value == 0)
            {
                _candidates = new HashSet<byte>(_allCandidates);
            }
        }

        public void RemoveCandidates(IEnumerable<byte> candidatesToRemove)
        {
            foreach (byte candidateToRemove in candidatesToRemove)
                if (_candidates.Remove(candidateToRemove))
                    CellChanged = true;

            if (_candidates.Count == 1)
                SetUniqueCandidate(_candidates.First());
        }

        public void SetUniqueCandidate(byte valueToSet)
        {
            Value = valueToSet;
            _candidates.Clear();
            CellChanged = true;
            Console.WriteLine($"Found answer! {valueToSet}");
        }

        public static void ResetCellChanged() =>
            CellChanged = false;

        public override string ToString() => 
            Value == 0 ? "-" : Value.ToString();
    }
}