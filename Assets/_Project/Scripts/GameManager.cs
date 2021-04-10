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
        [SerializeField] private CursorLock cursorLockScript;

        private void OnEnable()
        {
            restartGameEventChannel.OnEventRaised += RestartGame;
            exitGameEventChannel.OnEventRaised += ExitGame;
            gameOverEventChannel.OnEventRaised += HandleGameOver;
        }

        private void OnDisable()
        {
            restartGameEventChannel.OnEventRaised -= RestartGame;
            exitGameEventChannel.OnEventRaised -= ExitGame;
            gameOverEventChannel.OnEventRaised -= HandleGameOver;
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

        private void HandleGameOver()
        {
            cursorLockScript.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }
}