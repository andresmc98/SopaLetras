namespace Core.Events
{
    public class GameStartedEvent: GameEvent
    {
        public string LevelId { get; }
        public int WordCount { get; }

        public GameStartedEvent(string levelId, int wordCount)
        {
            LevelId = levelId;
            WordCount = wordCount;
        }
    }
    
    public class GamePausedEvent : GameEvent{}
    
    public class GameResumedEvent : GameEvent{}

    public class LevelCompleteEvent : GameEvent
    {
        public string LevelId { get; }
        public float CompletionTimeSeconds { get; }
        
        public LevelCompleteEvent(string levelId, float completionTimeSeconds)
        {
            LevelId = levelId;
            CompletionTimeSeconds = completionTimeSeconds;
        }
    }
    
    // WordSearch
    public class WordFoundEvent : GameEvent
    {
        public string Word { get; }
        public int WordsRemainingCount { get; }

        public WordFoundEvent(string word, int wordsRemainingCount)
        {
            Word = word;
            WordsRemainingCount = wordsRemainingCount;
        }
    }

    public class SelectionStartedEvent : GameEvent
    {
        public UnityEngine.Vector2Int GridCell { get; }
        
        public SelectionStartedEvent(UnityEngine.Vector2Int gridCell)
        {
            GridCell = gridCell;
        }
    }
    
    public class SelectionEndedEvent : GameEvent { }
    
    public class InvalidSelectionEvent : GameEvent { }
    
    // Pet System
    
    public class PetTappedEvent : GameEvent {}

    public class PlayerIdleTimeoutEvent : GameEvent
    {
        public float IdleSeconds { get; }

        public PlayerIdleTimeoutEvent(float idleSeconds)
        {
            IdleSeconds = idleSeconds;
        }
    }
    
    //UI
    
    public class HintRequestedEvent : GameEvent {}
    
    public class MainMenuOpenedEvent :  GameEvent {}

    public class LevelSelectedEvent : GameEvent
    {
        public string LevelId { get; }
        public LevelSelectedEvent(string levelId)
        {
            LevelId = levelId;
        }
    }
}