using System;
using Core.Events;
using Data.Words;
using UnityEngine;

namespace WordSearch
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GridGenerator _gridGenerator;
        [SerializeField] private WordValidator _wordValidator;

        private void Start()
        {
            _wordValidator.Init();
            _gridGenerator.GenerateGrid();
            
            EventBus.Publish(new GameStartedEvent("level_01", _wordValidator.WordsRemainingCount));
        }
    }
}