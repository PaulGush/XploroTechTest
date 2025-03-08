using System;
using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class MovementPoint : MonoBehaviour
    {
        private void Start()
        {
            name = "MovementPoint: " + transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}
