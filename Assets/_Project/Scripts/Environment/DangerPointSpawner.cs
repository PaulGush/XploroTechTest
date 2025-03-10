using System.Collections.Generic;
using _Project.Scripts.PlayerObject;
using _Project.Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Environment
{
    public class DangerPointSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform m_dangerPointParent;
        [SerializeField] private DangerPoint m_dangerPointPrefab;
        [SerializeField] private PatrolLinear m_patrolLinear;
        
        [Header("Values")]
        [SerializeField] private int m_amountToSpawn;
        [SerializeField] private float m_lowerBoundX, m_upperBoundX, m_lowerBoundZ, m_upperBoundZ;

        [Header("Debug Values")]
        [SerializeField] private List<DangerPoint> m_dangerPoints = new List<DangerPoint>();
        
        private void Start()
        {
            UIController.OnResetGame += Reset;

            m_patrolLinear.OnMovementPointsAssessed += SpawnDangerPoints;
        }

        private void SpawnDangerPoints(List<MovementPoint> movementPoints)
        {
            for (int i = 0; i < movementPoints.Count; i++)
            {
                if (movementPoints[i].transform.position.x < m_lowerBoundX)
                {
                    m_lowerBoundX = movementPoints[i].transform.position.x;
                }
                else if (movementPoints[i].transform.position.x > m_upperBoundX)
                {
                    m_upperBoundX = movementPoints[i].transform.position.x;
                }
                
                if (movementPoints[i].transform.position.z < m_lowerBoundZ)
                {
                    m_lowerBoundZ = movementPoints[i].transform.position.z;
                }
                else if (movementPoints[i].transform.position.z > m_upperBoundZ)
                {
                    m_upperBoundZ = movementPoints[i].transform.position.z;
                }
            }
            
            for (int i = 0; i < m_amountToSpawn; i++)
            {
                var dangerPoint = Instantiate(m_dangerPointPrefab,
                    new Vector3(Random.Range(m_lowerBoundX, m_upperBoundX), 0, Random.Range(m_lowerBoundZ, m_upperBoundZ)),
                    Quaternion.identity,
                    m_dangerPointParent);
                
                m_dangerPoints.Add(dangerPoint);
            }
        }

        private void Reset()
        {
            for (int i = 0; i < m_dangerPoints.Count; i++)
            {
                Destroy(m_dangerPoints[i].gameObject);
            }
            
            m_dangerPoints.Clear();
        }
    }
}
