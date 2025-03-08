using System;
using System.Collections.Generic;
using _Project.Scripts.Environment;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.PlayerObject
{
    public class PatrolLinear : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private NavMeshAgent m_navMeshAgent;
        [SerializeField] private List<MovementPoint> m_movementPoints;
        private readonly Queue<MovementPoint> m_movementPointQueue = new Queue<MovementPoint>();

        public Action<String> OnQueueEndReached;
        public Action<string> OnDangerPointCollision;

        private void Start()
        {
            AssessMovementPoints();
        }

        public void Reset()
        {
            AssessMovementPoints();
            m_navMeshAgent.isStopped = false;
        }

        private void AssessMovementPoints()
        {
            if (m_movementPoints.Count == 0) return;

            if (m_movementPointQueue.Count > 0)
            {
                m_movementPointQueue.Clear();
            }
            
            foreach (var movePoint in m_movementPoints)
            {
                m_movementPointQueue.Enqueue(movePoint);
            }

            SetDestinationToFirstInQueue();
        }

        private void SetDestinationToFirstInQueue()
        {
            m_navMeshAgent.SetDestination(m_movementPointQueue.Peek().transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MovementPoint movementPoint))
            {
                m_movementPointQueue.Dequeue();

                if (m_movementPointQueue.Count != 0)
                {
                    SetDestinationToFirstInQueue();
                }
                else
                {
                    //TODO Game complete screen
                    OnQueueEndReached?.Invoke("Game Complete!");
                    m_navMeshAgent.isStopped = true;
                }
            }
            
            if (other.TryGetComponent(out DangerPoint dangerPoint))
            {
                //TODO Game Over screen
                OnDangerPointCollision?.Invoke("Game Over!");
                m_navMeshAgent.isStopped = true;
            }
        }
    }
}
