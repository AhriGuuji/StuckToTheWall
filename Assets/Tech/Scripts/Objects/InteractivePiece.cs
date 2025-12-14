using UnityEngine;

namespace Tech.Scripts.Objects
{
    public class InteractivePiece : Interactive
    {
        [Header("Interactive Piece Settings")]
        [SerializeField] private float rotateAngle;
        [SerializeField] private float[] correctValue;
        
        public override void DoSomething()
        {
            transform.Rotate(0f, 0f, rotateAngle);
            
            base.DoSomething();
        }

        protected override bool PuzzleFinished()
        {
            foreach (float value in correctValue)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(transform.localEulerAngles.z, value)) < 0.1f)
                    return true;
            }
            return false;
        }
    }
}
