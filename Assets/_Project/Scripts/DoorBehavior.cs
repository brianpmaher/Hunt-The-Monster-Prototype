using UnityEngine;

namespace HuntTheMonster
{
    public class DoorBehavior : MonoBehaviour
    {
        private bool _isOpen;
        
        public void Open()
        {
            if (_isOpen) return;
            transform.Rotate(new Vector3(0, 90, 0));
            _isOpen = true;
        }
    }
}