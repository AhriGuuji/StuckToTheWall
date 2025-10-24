using UnityEngine;

public class SetNormal : MonoBehaviour
{
    [SerializeField]
    private Vector3 _normal;

    public Vector3 Normal => _normal.normalized;
}
