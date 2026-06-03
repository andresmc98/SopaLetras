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
}