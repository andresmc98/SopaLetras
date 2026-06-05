using System.Collections.Generic;
using Core.Events;
using Data.Words;
using UnityEngine;

namespace WordSearch
{
    public class WordValidator : MonoBehaviour
    {
        [SerializeField] private WordDatabase _wordDatabase;
        
        private HashSet<string> _wordsToFind;
        private HashSet<string> _wordsFound;

        public int WordsRemainingCount => _wordsToFind.Count;

        public void Init()
        {
            _wordsToFind = new HashSet<string>();
            _wordsFound = new HashSet<string>();

            foreach (var word in _wordDatabase.Words)
            {
                _wordsToFind.Add(word.ToUpper());
            }
        }

        public bool Validate(List<GridCell> selectedCells)
        {
            string selectedWord = BuildWord(selectedCells);
            string reversed = Reverse(selectedWord);

            string match = _wordsToFind.Contains(selectedWord) ? selectedWord :
                _wordsToFind.Contains(reversed) ? reversed : null;

            if (match == null)
            {
                EventBus.Publish(new InvalidSelectionEvent());
                return false;
            }

            _wordsToFind.Remove(match);
            _wordsFound.Add(match);

            foreach (var cell in selectedCells)
                cell.SetFound(true);
            
            EventBus.Publish(new WordFoundEvent(match, _wordsToFind.Count));
            
            if(_wordsToFind.Count == 0)
                EventBus.Publish(new LevelCompleteEvent("level_01", Time.time));
            
            return true;
        }

        private string BuildWord(List<GridCell> cells)
        {
            var sb = new System.Text.StringBuilder();
            foreach (var cell in cells)
                sb.Append(cell.Letter);
            return sb.ToString();
        }

        private string Reverse(string word)
        {
            char[] chars = word.ToCharArray();
            System.Array.Reverse(chars);
            return new string(chars);
        }
    }
}