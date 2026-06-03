using System;
using Core.Events;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

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

        private void Start()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu",
                UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }

        private void OnDestroy()
        {
            EventBus.Clear();
        }
    }
}