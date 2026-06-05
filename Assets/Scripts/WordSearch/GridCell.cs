using UnityEngine;
using TMPro;
using Core.Events;

namespace WordSearch
{
    public class GridCell : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _letterText;

        public char Letter { get; private set; }
        public int Row { get; private set; }
        public int Col { get; private set; }

        private Color _defaultColor;
        private Color _highlightColor = Color.yellow;
        private Color _foundColor = Color.green;

        private void Awake()
        {
            _defaultColor = _letterText.color;
        }

        public void Init(char letter, int row, int col)
        {
            Letter = letter;
            Row = row;
            Col = col;
            _letterText.text = letter.ToString();
        }

        public void SetHighlight(bool highlighted)
        {
            _letterText.color = highlighted ? _highlightColor : _defaultColor;
        }

        public void SetFound(bool found)
        {
            _letterText.color = found ? _foundColor : _defaultColor;
        }
    }
}