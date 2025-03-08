using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class DangerPoint : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}
