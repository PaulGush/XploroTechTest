using System.Collections.Generic;
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
        
        [Header("Values")]
        [SerializeField] private int m_amountToSpawn;
        [SerializeField] private float m_boundX, m_boundZ;

        [Header("Debug Values")]
        [SerializeField] private List<DangerPoint> m_dangerPoints = new List<DangerPoint>();
        
        private void Start()
        {
            UIController.OnResetGame += Reset;

            SpawnDangerPoints();
        }

        private void SpawnDangerPoints()
        {
            for (int i = 0; i < m_amountToSpawn; i++)
            {
                var dangerPoint = Instantiate(m_dangerPointPrefab,
                    new Vector3(Random.Range(-m_boundX, m_boundX), 0, Random.Range(-m_boundZ, m_boundZ)),
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
            
            SpawnDangerPoints();
        }
    }
}
