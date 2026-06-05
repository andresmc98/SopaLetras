using Data.Words;
using UnityEngine;

namespace WordSearch
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GridCell _cellPrefab;
        [SerializeField] private WordDatabase _wordDatabase;
        [SerializeField] private int _gridSize = 10;
        [SerializeField] private float _cellSize = 1f;

        private GridCell[,] _grid;
        private char[,] _letters;
        
        public GridCell[,] Grid => _grid;
        public int GridSize => _gridSize;

        public void GenerateGrid()
        {
            _letters = new char[_gridSize, _gridSize];
            _grid = new GridCell[_gridSize, _gridSize];

            PlaceWords();
            FillEmptyCells();
            SpawnCells();
        }

        private void PlaceWords()
        {
            foreach (var word in _wordDatabase.Words)
                TryPlaceWord(word.ToUpper());
        }

        private bool TryPlaceWord(string word)
        {
            int maxAttempts = 100;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                bool horizontal = Random.value > 0.5f;
                int row, col;

                if (horizontal)
                {
                    row = Random.Range(0, _gridSize);
                    col = Random.Range(0, _gridSize - word.Length);
                }
                else
                {
                    row = Random.Range(0, _gridSize - word.Length);
                    col = Random.Range(0, _gridSize);
                }

                if (CanPlace(word, row, col, horizontal))
                {
                    Place(word, row, col, horizontal);
                    return true;
                }
            }
            
            Debug.LogWarning($"No se pudo colocar la palabra: {word}");
            return false;
        }

        private bool CanPlace(string word, int row, int col, bool horizontal)
        {
            for (int i = 0; i < word.Length; i++)
            {
                int r = horizontal ? row : row + i;
                int c = horizontal ? col + i : col;
                char existing = _letters[r, c];

                if (existing != '\0' && existing != word[i])
                {
                    return false;
                }
            }
            return true;
        }

        private void Place(string word, int row, int col, bool horizontal)
        {
            for (int i = 0; i < word.Length; i++)
            {
                int r = horizontal ? row : row + i;
                int c = horizontal ? col + i : col;
                _letters[r, c] = word[i];
            }
        }

        private void FillEmptyCells()
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int r = 0; r < _gridSize; r++)
            for (int c = 0; c < _gridSize; c++)
                if (_letters[r, c] == '\0')
                    _letters[r, c] = alphabet[Random.Range(0, alphabet.Length)];
        }
        
        private void SpawnCells()
        {
            float offset = (_gridSize - 1) * _cellSize * 0.5f;

            for (int r = 0; r < _gridSize; r++)
            {
                for (int c = 0; c < _gridSize; c++)
                {
                    Vector3 pos = new Vector3(c * _cellSize - offset, -r * _cellSize + offset, 0);
                    GridCell cell = Instantiate(_cellPrefab, pos, Quaternion.identity, transform);
                    cell.Init(_letters[r, c], r, c);
                    _grid[r, c] = cell;
                }
            }
        }
    }
}