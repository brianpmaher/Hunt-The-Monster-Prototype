using HuntTheMonster.EventChannels;
using UnityEngine;
using UnityEngine.UI;

namespace HuntTheMonster.UI
{
    public class StartScreenContainer : MonoBehaviour
    {
        [SerializeField] private GameObject startScreen;
        [SerializeField] private Button startButton;
        [SerializeField] private VoidEventChannel startGameChannel;

        private void OnEnable()
        {
            startButton.onClick.AddListener(HandleStart);
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveListener(HandleStart);
        }

        private void HandleStart()
        {
            startGameChannel.RaiseEvent();
            startScreen.SetActive(false);
        }
    }
}
