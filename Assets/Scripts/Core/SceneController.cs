using Core.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            EventBus.Subscribe<LevelSelectedEvent>(OnLevelSelected);
            EventBus.Subscribe<MainMenuOpenedEvent>(OnMainMenuOpened);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<LevelSelectedEvent>(OnLevelSelected);
            EventBus.Unsubscribe<MainMenuOpenedEvent>(OnMainMenuOpened);
        }

        private void OnLevelSelected(LevelSelectedEvent e)
        {
            LoadScene("Game");
        }

        private void OnMainMenuOpened(MainMenuOpenedEvent e)
        {
            LoadScene("MainMenu");
        }

        private void LoadScene(string sceneName)
        {
            EventBus.Clear();
            SceneManager.LoadScene(sceneName);
        }
    }
}