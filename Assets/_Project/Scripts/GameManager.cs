using HuntTheMonster.EventChannels;
using HuntTheMonster.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HuntTheMonster
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private VoidEventChannel restartGameEventChannel;
        [SerializeField] private VoidEventChannel exitGameEventChannel;
        [SerializeField] private VoidEventChannel gameOverEventChannel;
        [SerializeField] private VoidEventChannel gameWinEventChannel;
        [SerializeField] public CursorLock cursorLockScript;

        private void OnEnable()
        {
            restartGameEventChannel.OnEventRaised += RestartGame;
            exitGameEventChannel.OnEventRaised += ExitGame;
            gameOverEventChannel.OnEventRaised += HandleGameEnd;
            gameWinEventChannel.OnEventRaised += HandleGameEnd;
        }

        private void OnDisable()
        {
            restartGameEventChannel.OnEventRaised -= RestartGame;
            exitGameEventChannel.OnEventRaised -= ExitGame;
            gameOverEventChannel.OnEventRaised -= HandleGameEnd;
            gameWinEventChannel.OnEventRaised -= HandleGameEnd;
        }

        private void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("GeneratedScene");
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void HandleGameEnd()
        {
            cursorLockScript.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }
}