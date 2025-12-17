using System.Collections.Generic;
using Tech.Scripts.Objects;
using UnityEngine;

namespace Tech.Scripts.PuzzleSetUp
{
    public class ShowToAlien : MonoBehaviour
    {
        [SerializeField] private Animator alienAnimator;
        [SerializeField] private InteractiveData alienState;
        [SerializeField] private Interactive alienCurrentState;
        [SerializeField] private InteractiveMug mug;
        private bool _isHappy;

        private void Start()
        {
            _isHappy = false;
        }

        private void OnTriggerStay(Collider other)
        {
            if(alienCurrentState?.interactiveData?.name == "HappyAlien")
                if (other.TryGetComponent(out PlayerInventory playerInventory))
                {
                    if (playerInventory?.GetSelected()?.interactiveData is InteractiveChemicData chemic)
                    {
                        if (!_isHappy 
                            && mug.Solution.Contains(chemic.color) 
                            && !mug.ActualLiquids.Contains(chemic.name))
                        {
                            alienAnimator.SetBool("Dance",true);
                            _isHappy = true;
                        }
                    }

                    if (_isHappy && playerInventory.GetSelected().interactiveData is not InteractiveChemicData)
                    {
                        alienAnimator.SetBool("Dance", false);
                        _isHappy = false;
                    }
                }
        }

        private void OnTriggerExit(Collider other)
        {
            alienAnimator.SetBool("Dance", false);
            _isHappy = false;
        }
    }
}