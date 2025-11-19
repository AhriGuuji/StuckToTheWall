using UnityEngine;

namespace Objects
{
    public class InteractivePiece : Interactive
    {
        [Header("Interactive Piece Settings")]
        [SerializeField] private float rotateAngle;
        //[SerializeField] private float rotateSpeed;
        [SerializeField] private float correctValue;
        public override void DoSomething()
        {
            transform.Rotate(0f, 0f, rotateAngle);
        }
    }
}
