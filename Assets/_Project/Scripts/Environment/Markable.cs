using UnityEngine;

namespace HuntTheMonster.Environment
{
    public class Markable : MonoBehaviour
    {
        [SerializeField] private Transform[] markablePositions;
        [SerializeField] private GameObject markerPrefab;

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
            }
        }
    }
}