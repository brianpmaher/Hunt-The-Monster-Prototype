using HuntTheMonster.EventChannels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HuntTheMonster.UI
{
    public class GameOverContainer : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private VoidEventChannel restartGameEventChannel;
        [SerializeField] private VoidEventChannel exitGameEventChannel;
        [SerializeField] private VoidEventChannel gameOverEventChannel;

        private void OnEnable()
        {
            retryButton.onClick.AddListener(HandleRetry);
            exitButton.onClick.AddListener(HandleExit);
            gameOverEventChannel.OnEventRaised += HandleGameOver;
        }

        private void OnDisable()
        {
            retryButton.onClick.RemoveListener(HandleRetry);
            exitButton.onClick.RemoveListener(HandleExit);
            gameOverEventChannel.OnEventRaised -= HandleGameOver;
        }

        private void HandleRetry()
        {
            restartGameEventChannel.RaiseEvent();
        }

        private void HandleExit()
        {
            exitGameEventChannel.RaiseEvent();
        }

        private void HandleGameOver()
        {
            gameOverScreen.SetActive(true);
        }
    }
}