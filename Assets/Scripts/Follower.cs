using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Follower : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Vector3 followOffset;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        
        public void Follow()
        {
            followOffset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-6f, -2f));
            _agent.SetDestination(transform.parent.position + followOffset);
            transform.LookAt(transform.parent.forward);
            transform.rotation = transform.parent.rotation;
        }
        

        private void OnEnable()
        {
            followOffset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-6f, -2f));
            transform.localPosition = followOffset;
        }
    }
}