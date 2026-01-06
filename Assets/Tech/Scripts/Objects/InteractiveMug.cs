using System.Collections.Generic;
using SmallHedge.SoundManager;
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
            {
                return;
            }

            if(_strings.Count <= solution.Count)
                _strings.Add(chemic.color);

            SoundManager.PlaySound(SoundType.BOTTLE);

            foreach (string s in _strings)
                print(s);

            base.DoSomething();

            VerifyComplete += OnVerifyComplete;
        }

        public override void Drop()
        {
            base.Drop();

            if(_strings.Count == 0)
                _interactiveData.type = InteractiveData.Type.InteractMulti;
        }

        private void OnVerifyComplete()
        {
            PuzzleFinished();
            VerifyComplete -= OnVerifyComplete;
        }

        protected override bool PuzzleFinished()
        {
            if (_strings.Count < solution.Count)
                return false;

            _interactiveData.type = InteractiveData.Type.Pickable;

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

        public void Clear()
        {
            _strings = new List<string>();
        }
    }
}