using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tech.Scripts.Objects
{
    public class InteractiveMug : Interactive
    {
        [SerializeField] private List<string> solution = new ();
        private List<string> _strings = new ();

        private void Start()
        {
            _interactiveData.type = InteractiveData.Type.InteractMulti;
        }

        public override void DoSomething()
        {
            if (_playerInventory.GetSelected().interactiveData is InteractiveChemicData chemic)
            {
                _strings.Add(chemic.color);
                
                base.DoSomething();

                VerifyComplete += OnVerifyComplete;
            }
        }

        private void OnVerifyComplete()
        {
            PuzzleFinished();
            VerifyComplete -= OnVerifyComplete;
        }

        protected override bool PuzzleFinished()
        {
            if (_strings.Count == solution.Count)
                if (_strings[^1] == solution[^1])
                {
                    _interactiveData.type = InteractiveData.Type.Pickable;
                    Debug.Log("All chemics are right!");
                    return true;
                }
                else
                {
                    Debug.Log("Do it again!");
                    _strings = new List<string>();
                }
            
            return false;
        }
    }
}