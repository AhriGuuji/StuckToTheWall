using System;
using System.Collections.Generic;
using System.Linq;
using Tech.Scripts.Objects;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public bool IsComplete => PuzzleCompleted();
    [SerializeField] private List<Interactive> puzzlePieces;
    private readonly Func<IEnumerable<Interactive>, bool> _puzzleCompleted = pieces =>
        pieces.All(puzzlePiece => puzzlePiece.IsDone);

    private void OnPuzzlePieceChanged()
    {
        Debug.Log("oooi");
        if (PuzzleCompleted())
        {
            DoPuzzleReward();
            UnsubscribeFromEvents();
        }
    }

    private bool PuzzleCompleted()
    {
        return _puzzleCompleted(puzzlePieces);
    }

    private void Start()
    {
        foreach (Interactive piece in puzzlePieces)
        {
            piece.VerifyComplete += OnPuzzlePieceChanged;
        }
    }

    private void UnsubscribeFromEvents()
    {
        foreach (Interactive piece in puzzlePieces)
        {
            piece.VerifyComplete -= OnPuzzlePieceChanged;
        }
    }

    protected virtual void DoPuzzleReward(){}
}