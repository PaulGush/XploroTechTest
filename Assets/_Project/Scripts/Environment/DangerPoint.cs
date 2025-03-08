using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class DangerPoint : MonoBehaviour
    {
        private void Start()
        {
            name = "DangerPoint: " + transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}
