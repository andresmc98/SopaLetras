namespace Core.Events
{
    public abstract class GameEvent
    {
        public float TimeStamp { get; }

        protected GameEvent()
        {
            TimeStamp = UnityEngine.Time.time;
        }
    }
}