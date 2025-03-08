using UnityEngine;

namespace _Project.Scripts.Environment
{
    public class MovementPoint : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}
