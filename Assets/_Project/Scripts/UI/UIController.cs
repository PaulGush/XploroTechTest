using System;
using _Project.Scripts.PlayerObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PatrolLinear m_patrolLinear;
        [SerializeField] private TextMeshProUGUI m_endText;
        [SerializeField] private Button m_resetButton;

        private void Start()
        {
            m_patrolLinear.OnDangerPointCollision += ResolveEndGame;
            m_patrolLinear.OnQueueEndReached += ResolveEndGame;
            DisableResetButton();
            SetEndText(String.Empty);
        }

        private void ResolveEndGame(string newValue)
        {
            SetEndText(newValue);
            m_resetButton.gameObject.SetActive(true);
        }

        private void SetEndText(string newValue)
        {
            m_endText.text = newValue;
        }

        private void DisableResetButton()
        {
            m_resetButton.gameObject.SetActive(false);
        }

        public void ResetGame()
        {
            SetEndText(String.Empty);
            m_resetButton.gameObject.SetActive(false);
            m_patrolLinear.gameObject.transform.position = Vector3.zero;
            m_patrolLinear.Reset();
        }
    }
}
