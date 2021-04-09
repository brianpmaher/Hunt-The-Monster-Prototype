using HuntTheMonster.EventChannels;
using UnityEngine;
using UnityEngine.UI;

namespace HuntTheMonster
{
    public class UIGameOverContainerBehavior : MonoBehaviour
    {
        [SerializeField] private Button retryButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private VoidEventChannelSO restartGameEventChannel;
        [SerializeField] private VoidEventChannelSO exitGameEventChannel;

        private void OnEnable()
        {
            retryButton.onClick.AddListener(HandleRetry);
            exitButton.onClick.AddListener(HandleExit);
        }

        private void OnDisable()
        {
            retryButton.onClick.RemoveListener(HandleRetry);
            exitButton.onClick.RemoveListener(HandleExit);
        }

        private void HandleRetry()
        {
            restartGameEventChannel.RaiseEvent();
        }

        private void HandleExit()
        {
            exitGameEventChannel.RaiseEvent();
        }
    }
}