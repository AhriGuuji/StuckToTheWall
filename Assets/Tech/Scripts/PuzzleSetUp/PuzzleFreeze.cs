using SmallHedge.SoundManager;
using Tech.Scripts.Objects;
using UnityEngine;

namespace DefaultNamespace
{
    public class PuzzleFreeze : Puzzle
    {
        [Header("This Puzzle Settings")]
        [SerializeField] private GameObject toSetActive;
        [SerializeField] private GameObject toSetUnactive;

        public override void CHEAT()
        {
            DoPuzzleReward();
        }

        protected override void DoPuzzleReward()
        {
            SoundManager.PlaySound(SoundType.FREEZE);
            foreach(Interactive piece in puzzlePieces)
                piece.gameObject.SetActive(false);
            toSetUnactive.SetActive(false);
            toSetActive.SetActive(true);
        }
    }
}