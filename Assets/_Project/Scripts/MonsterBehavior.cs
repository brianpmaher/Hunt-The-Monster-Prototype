using UnityEngine;

namespace HuntTheMonster
{
    public class MonsterBehavior : MonoBehaviour
    {
        private Transform _playerTransform;
        
        private void Start()
        {
            _playerTransform = PlayerManagerBehavior.PlayerTransform;
        }

        private void Update()
        {
            FaceThePlayer();
        }

        private void FaceThePlayer()
        {
            transform.LookAt(_playerTransform);
        }
    }
}