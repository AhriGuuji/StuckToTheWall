using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<Puzzle> _puzzlePieces;
    int _cheatIDX;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && _cheatIDX < _puzzlePieces.Count())
        {
            _puzzlePieces[_cheatIDX].CHEAT();
            _cheatIDX++;
        }
    }
}
