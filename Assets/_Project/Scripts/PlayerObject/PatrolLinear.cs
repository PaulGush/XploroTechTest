using System;
using System.Collections.Generic;
using _Project.Scripts.Environment;
using _Project.Scripts.UI;
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

        private bool m_collidedWithDangerPoint = false;
        
        public Action<string> OnQueueEndReached;
        public Action<string> OnDangerPointCollision;
        public Action<List<MovementPoint>> OnMovementPointsAssessed;

        private void Start()
        {
            AssessMovementPoints();
            UIController.OnResetGame += Reset;
        }

        public void Reset()
        {
            m_collidedWithDangerPoint = false;
            transform.position = Vector3.zero;
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

            OnMovementPointsAssessed?.Invoke(m_movementPoints);
            SetDestinationToFirstInQueue();
        }

        private void SetDestinationToFirstInQueue()
        {
            m_navMeshAgent.SetDestination(m_movementPointQueue.Peek().transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (m_collidedWithDangerPoint) return;
            
            if (other.TryGetComponent(out DangerPoint dangerPoint))
            {
                m_navMeshAgent.isStopped = true;
                m_collidedWithDangerPoint = true;
                OnDangerPointCollision?.Invoke("Game Over!");
            }
            
            if (other.TryGetComponent(out MovementPoint movementPoint))
            {
                if (m_movementPointQueue.Count != 0)
                {
                    m_movementPointQueue.Dequeue();
                }
                
                if (m_movementPointQueue.Count != 0)
                {
                    SetDestinationToFirstInQueue();
                }
                else
                {
                    m_navMeshAgent.isStopped = true;
                    OnQueueEndReached?.Invoke("Game Complete!");
                }
            }
        }
    }
}
