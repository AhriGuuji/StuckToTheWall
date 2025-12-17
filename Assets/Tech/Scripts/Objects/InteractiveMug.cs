using System.Collections.Generic;
using UnityEngine;

namespace Tech.Scripts.Objects
{
    public class InteractiveMug : Interactive
    {
        [SerializeField] private List<string> solution;
        private List<string> _strings;
        public List<string> ActualLiquids => _strings;
        public List<string> Solution => solution;

        private void Start()
        {
            _interactiveData.type = InteractiveData.Type.InteractMulti;
            _strings = new List<string>();
        }

        public override void DoSomething()
        {
            if (_playerInventory?.GetSelected()?.interactiveData is not InteractiveChemicData chemic) 
                return;
            
            _strings.Add(chemic.color);

            base.DoSomething();

            VerifyComplete += OnVerifyComplete;
        }

        private void OnVerifyComplete()
        {
            PuzzleFinished();
            VerifyComplete -= OnVerifyComplete;
        }

        protected override bool PuzzleFinished()
        {
            if (_strings.Count != solution.Count)
                return false;

            for (int i = 0; i < solution.Count; i++)
            {
                if (_strings[i] != solution[i])
                {
                    _strings = new List<string>();
                    return false;
                }
            }
            
            return true;
        }

        protected override void PlayAnimation(string animation)
        {
            if(_playerInventory?.GetSelected()?.interactiveData is InteractiveChemicData)
                base.PlayAnimation(animation);
        }
    }
}