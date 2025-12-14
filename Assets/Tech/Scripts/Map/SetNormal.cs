using UnityEngine;

namespace Tech.Scripts.Map
{
    public class SetNormal : MonoBehaviour
    {
        [SerializeField]
        private Vector3 normal;

        public Vector3 Normal => normal.normalized;
    }
}
