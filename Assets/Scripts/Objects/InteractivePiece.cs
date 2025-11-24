using UnityEngine;

namespace Objects
{
    public class InteractivePiece : Interactive
    {
        [Header("Interactive Piece Settings")]
        [SerializeField] private float rotateAngle;
        //[SerializeField] private float rotateSpeed;
        [SerializeField] private float correctValue;
        [SerializeField] private Puzzle puzzle;
        public override void DoSomething()
        {
            puzzle.VerifyComplete += puzzle.OnTryPuzzle;
            
            transform.Rotate(0f, 0f, rotateAngle);
        }
    }
}
