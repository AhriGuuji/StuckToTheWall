using UnityEngine;

namespace Objects
{
    public class InteractivePiece : Interactive
    {
        [Header("Interactive Piece Settings")]
        [SerializeField] private float rotateAngle;
        [SerializeField] private float[] correctValue;
        [SerializeField] private Puzzle puzzle;
        
        public override void DoSomething()
        {
            transform.Rotate(0f, 0f, rotateAngle);
            
            base.DoSomething();
        }

        protected override bool PuzzleFinished()
        {
            foreach (float a in correctValue)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(transform.localEulerAngles.z, a)) < 0.1f)
                    return true;
            }
            return false;
        }
    }
}
