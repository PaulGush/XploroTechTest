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

        private void Start()
        {
            AssessMovementPoints();
        }
        
        private void AssessMovementPoints()
        {
            if (m_movementPoints.Count == 0) return;
            
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
            }
        }
    }
}
