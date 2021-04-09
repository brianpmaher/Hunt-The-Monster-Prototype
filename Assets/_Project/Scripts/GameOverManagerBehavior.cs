using HuntTheMonster.EventChannels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HuntTheMonster
{
    public class GameOverManagerBehavior : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO restartGameEventChannel;
        [SerializeField] private VoidEventChannelSO exitGameEventChannel;

        private void OnEnable()
        {
            restartGameEventChannel.OnEventRaised += RestartGame;
            exitGameEventChannel.OnEventRaised += ExitGame;
        }

        private void OnDisable()
        {
            restartGameEventChannel.OnEventRaised -= RestartGame;
            exitGameEventChannel.OnEventRaised -= ExitGame;
        }

        private void RestartGame()
        {
            SceneManager.LoadScene("HuntTheMonsterScene");
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}