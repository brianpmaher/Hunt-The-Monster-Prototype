using System.Collections;
using UnityEngine;

namespace HuntTheMonster.Environment
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private float openAnimationSeconds = 1f;

        private bool _isOpening;
        private bool _isOpen;

        public void Open()
        {
            if (_isOpen || _isOpening) return;
            StartCoroutine(OpenDoorAnimation());
        }

        private IEnumerator OpenDoorAnimation()
        {
            _isOpening = true;

            var elapsedSeconds = 0f;

            while (elapsedSeconds < openAnimationSeconds)
            {
                elapsedSeconds += Time.deltaTime;
                var rate = Time.deltaTime / openAnimationSeconds;
                var rotation = new Vector3(0, rate * 90, 0);
                transform.Rotate(rotation);
                yield return null;
            }
            
            _isOpening = false;
            _isOpen = true;
        }
    }
}