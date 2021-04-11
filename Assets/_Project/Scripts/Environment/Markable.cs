using HuntTheMonster.EventChannels;
using UnityEngine;

namespace HuntTheMonster.Environment
{
    public class Markable : MonoBehaviour
    {
        [SerializeField] private Transform[] markablePositions;
        [SerializeField] private GameObject markerPrefab;
        [SerializeField] private VoidEventChannel gameWinEventChannel;
        [SerializeField] private VoidEventChannel gameOverEventChannel;
        [SerializeField] private bool isMonsterDoor;
        
        private bool _marked;

        public void Mark()
        {
            if (!_marked)
            {
                foreach (var markablePosition in markablePositions)
                {
                    Instantiate(markerPrefab, markablePosition);
                }

                _marked = true;

                if (isMonsterDoor)
                {
                    gameWinEventChannel.RaiseEvent();
                }
                else
                {
                    gameOverEventChannel.RaiseEvent();
                }
            }
        }
    }
}