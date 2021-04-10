using System.Collections;
using HuntTheMonster.EventChannels;
using HuntTheMonster.Player;
using UnityEngine;

namespace HuntTheMonster.Monster
{
    public class MonsterManager : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float endGameDistance = 1f;
        [SerializeField] private VoidEventChannel gameOverChannel;
        [SerializeField] private Transform rayOrigin;
            
        private Transform _playerTransform;
        private bool _movingTowardPlayer;
        
        private void Start()
        {
            _playerTransform = PlayerManager.PlayerTransform;
        }

        private void Update()
        {
            FaceThePlayer();

            if (CanSeePlayer() && !_movingTowardPlayer)
            {
                ChasePlayer();
            }
        }

        private void FaceThePlayer()
        {
            transform.LookAt(_playerTransform);
        }

        private bool CanSeePlayer()
        {
            var position = rayOrigin.position;
            var distanceToPlayer = (transform.position - _playerTransform.position).magnitude;
            var direction = rayOrigin.forward * distanceToPlayer;
            var ray = new Ray(position, direction);
            
            if (Physics.Raycast(ray, out var hit))
            {
                return hit.collider.CompareTag("Player");
            }

            return false;
        }

        private void ChasePlayer()
        {
            _movingTowardPlayer = true;
            StartCoroutine(MoveTowardPlayer());
        }

        private IEnumerator MoveTowardPlayer()
        {
            var direction = transform.position - _playerTransform.position;
            var distanceToPlayer = direction.magnitude;

            if (distanceToPlayer <= endGameDistance)
            {
                gameOverChannel.RaiseEvent();
                yield break;
            }

            var movement = moveSpeed * Time.deltaTime * direction.normalized;
            transform.position -= movement;
            yield return null;
            yield return MoveTowardPlayer();
        }
    }
}