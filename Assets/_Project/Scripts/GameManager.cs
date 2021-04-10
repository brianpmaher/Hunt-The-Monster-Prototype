using HuntTheMonster.EventChannels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HuntTheMonster
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private VoidEventChannel restartGameEventChannel;
        [SerializeField] private VoidEventChannel exitGameEventChannel;

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