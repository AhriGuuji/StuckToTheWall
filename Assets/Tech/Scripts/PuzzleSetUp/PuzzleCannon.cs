using UnityEngine;

namespace DefaultNamespace
{
    public class PuzzleCannon : Puzzle
    {
        [Header("This Puzzle Settings")]
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private LineRenderer line;
        public bool IsOn { get; private set; }
        protected override void DoPuzzleReward()
        {
            IsOn = true;
            line.enabled = true;
            line.SetPosition(0,startPoint.position);
            line.SetPosition(1,endPoint.position);
        }
    }
}