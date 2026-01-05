using Tech.Scripts.Objects;
using UnityEngine;

namespace DefaultNamespace
{
    public class PuzzleCrystal : Puzzle
    {
        [Header("This Puzzle Settings")]
        [SerializeField] private GameObject toSetActive;
        protected override void DoPuzzleReward()
        {
            foreach(Interactive piece in puzzlePieces)
                piece.gameObject.SetActive(false);
            toSetActive.SetActive(true);
        }
    }
}