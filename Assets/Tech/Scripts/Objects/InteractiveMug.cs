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
            bool isRight = true;
            
            if (_strings.Count == solution.Count)
                for (int i = 0; i < solution.Count; i++)
                {
                    if (_strings[^1] == solution[^1] && isRight)
                    {
                        _interactiveData.type = InteractiveData.Type.Pickable;
                        return true;
                    }
                    
                    if (_strings[i] != solution[i]) 
                        isRight = false;

                    if (i == solution.Count - 1)
                    {
                        _strings = new List<string>();
                    }
                }
            
            return false;
        }
    }
}