using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Puzzle
{
    public bool IsComplete => PuzzleCompleted();
    [SerializeField] private readonly IEnumerable<Interactive> _puzzlePieces;
    private readonly Func<IEnumerable<Interactive>, bool> _puzzleCompleted = pieces =>
        pieces.All(puzzlePiece => puzzlePiece.IsDone);
        
    public event Action VerifyComplete;

    public Puzzle(IEnumerable<Interactive> puzzlePieces)
    {
        _puzzlePieces = puzzlePieces;
    }

    public void OnTryPuzzle()
    {
        if (PuzzleCompleted())
            DoPuzzleReward();
    }

    private bool PuzzleCompleted()
    {
        VerifyComplete -= OnTryPuzzle;
        return _puzzleCompleted(_puzzlePieces);
    }

    private void Start()
    {
        VerifyComplete?.Invoke();
    }

    protected virtual void DoPuzzleReward(){}
}