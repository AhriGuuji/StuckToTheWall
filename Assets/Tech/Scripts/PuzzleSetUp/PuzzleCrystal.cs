using SmallHedge.SoundManager;
using Tech.Scripts.Objects;
using UnityEngine;

namespace DefaultNamespace
{
    public class PuzzleCrystal : Puzzle
    {
        [Header("This Puzzle Settings")]
        [SerializeField] private GameObject toSetActive;
        [SerializeField] private GameObject toSetUnactive;
        protected override void DoPuzzleReward()
        {
            SoundManager.PlaySound(SoundType.ROCK);
            foreach(Interactive piece in puzzlePieces)
                piece.gameObject.SetActive(false);
            toSetUnactive.SetActive(false);
            toSetActive.SetActive(true);
        }
    }
}