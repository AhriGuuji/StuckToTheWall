using SmallHedge.SoundManager;
using UnityEngine;

namespace DefaultNamespace
{
    public class PuzzleCannon : Puzzle
    {
        [Header("This Puzzle Settings")]
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private LineRenderer line;

        public override void CHEAT()
        {
            DoPuzzleReward();
        }

        protected override void DoPuzzleReward()
        {
            line.enabled = true;
            line.SetPosition(0,startPoint.position);
            line.SetPosition(1,endPoint.position);
            SoundManager.PlaySound(SoundType.LASER);
        }
    }
}