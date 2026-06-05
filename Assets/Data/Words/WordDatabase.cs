using UnityEngine;

namespace Data.Words
{
    [CreateAssetMenu(fileName = "WordDatabase", menuName = "SopaLetras/Word Database")]
    public class WordDatabase : ScriptableObject
    {
        [SerializeField] private string[] _words;

        public string[] Words => _words;
    }
}