using System.Collections.Generic;
using Tech.Scripts.Objects;
using UnityEngine;

namespace Tech.Scripts.PuzzleSetUp
{
    public class ShowToAlien : MonoBehaviour
    {
        [SerializeField] private List<string> solution;
        [SerializeField] private Animator alienAnimator;
        [SerializeField] private InteractiveData alienState;
        [SerializeField] private Interactive alienCurrentState;
        private bool _isHappy;

        private void Start()
        {
            _isHappy = false;
        }

        private void OnTriggerStay(Collider other)
        {
            
            
            Debug.Log(_isHappy);
            
            if(alienCurrentState.interactiveData.name == "HappyAlien")
                if (other.TryGetComponent(out PlayerInventory playerInventory))
                {
                    if (playerInventory?.GetSelected()?.interactiveData is InteractiveChemicData chemic)
                    {
                        if (!_isHappy && solution.Contains(chemic.color))
                        {
                            Debug.Log(chemic.color);
                            alienAnimator.SetBool("Dance",true);
                            _isHappy = true;
                        }
                    }

                    if (_isHappy && playerInventory.GetSelected().interactiveData is not InteractiveChemicData)
                    {
                        Debug.Log(playerInventory.GetSelected().interactiveData);
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