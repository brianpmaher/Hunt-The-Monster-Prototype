using HuntTheMonster.EventChannels;
using UnityEngine;
using UnityEngine.UI;

namespace HuntTheMonster.UI
{
    public class GameWinContainer : MonoBehaviour
    {
        [SerializeField] private GameObject gameWinScreen;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private VoidEventChannel restartGameEventChannel;
        [SerializeField] private VoidEventChannel exitGameEventChannel;
        [SerializeField] private VoidEventChannel gameWinEventChannel;

        private void OnEnable()
        {
            retryButton.onClick.AddListener(HandleRetry);
            exitButton.onClick.AddListener(HandleExit);
            gameWinEventChannel.OnEventRaised += HandleGameWin;
        }

        private void OnDisable()
        {
            retryButton.onClick.RemoveListener(HandleRetry);
            exitButton.onClick.RemoveListener(HandleExit);
            gameWinEventChannel.OnEventRaised -= HandleGameWin;
        }

        private void HandleRetry()
        {
            restartGameEventChannel.RaiseEvent();
        }

        private void HandleExit()
        {
            exitGameEventChannel.RaiseEvent();
        }

        private void HandleGameWin()
        {
            gameWinScreen.SetActive(true);
        }
    }
}