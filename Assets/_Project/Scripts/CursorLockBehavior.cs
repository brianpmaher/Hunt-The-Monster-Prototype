using UnityEngine;
using Cursor = UnityEngine.Cursor;

namespace HuntTheMonster
{
    public class CursorLockBehavior : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                LockCursor();
            }

            if (Input.GetButtonDown("Cancel"))
            {
                UnlockCursor();
            }
        }

        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void UnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}