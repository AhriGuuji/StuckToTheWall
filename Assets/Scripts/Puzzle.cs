using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DefaultNamespace
{
    public class Puzzle
    {
        [SerializeField] private IEnumerable<Interactive> puzzlePieces;
        
        public bool PuzzleCompleted()
        {
            Func<IEnumerable<Interactive>, bool> puzzleCompleted = pieces =>
                pieces.All(puzzlePiece => puzzlePiece.IsDone);
    
            return puzzleCompleted(puzzlePieces);
        }
    }
}