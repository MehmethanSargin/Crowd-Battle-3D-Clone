using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class CrowdController : MonoBehaviour
    {
        public int CurrentCount => _currentCount;

        private ObjectPool<Follower> _pool;
        private List<Follower> _activeFollowers;
        private int _currentCount = 1;

        public CrowdController(Follower follower, int maxFollowerCount, Transform parent)
        {
            _pool = new ObjectPool<Follower>(follower, maxFollowerCount, parent);
            _activeFollowers = new List<Follower>();
        }

        public void Follow()
        {
            foreach (var follower in _activeFollowers)
            {
                follower.Follow();
            }
        }
        
        public void AddToCrowd(Gate.GateType gateType, int value)
        {
            int runToAdd = 0;
            if (gateType == Gate.GateType.Add) runToAdd = value;
            else if (gateType == Gate.GateType.Multiply) runToAdd = _currentCount * (value - 1);

            for (int i = 0; i < runToAdd; i++)
            {
                Follower follower = _pool.GetObject();
                _activeFollowers.Add(follower);
            }

            _currentCount += runToAdd;
        }
        
        public void AddToCrowd(int value)
        {
            for (int i = 0; i < value; i++)
            {
                Follower follower = _pool.GetObject();
                _activeFollowers.Add(follower);
            }

            _currentCount += value;
        }

        public void RemoveFromCrowd()
        {
            _currentCount--;
            if (_activeFollowers.Count == 0)
            {
              return;  
            }
            Follower follower = _activeFollowers.Last();
            follower.gameObject.SetActive(false);
            _activeFollowers.RemoveAt(_activeFollowers.Count-1);
        }
    }
}