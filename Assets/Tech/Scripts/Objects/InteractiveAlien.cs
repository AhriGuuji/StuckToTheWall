using UnityEngine;
using UnityEngine.UI;

namespace Tech.Scripts.Objects
{
    public class InteractiveAlien : Interactive
    {
        [Header("Alien Settings")] 
        [SerializeField] private Sprite angryFace;
        [SerializeField] private Sprite happyFace;
        [SerializeField] private Image face;
        [SerializeField] private InteractiveData angryAlien;
        [SerializeField] private InteractiveData happyAlien;

        public override void DoSomething()
        {
            if (_interactiveData.name == "SleepyAlien")
            {
                face.sprite = angryFace;
                _interactiveData = angryAlien;
                return;
            }

            if (_interactiveData.name == "AngryAlien")
            {
                face.sprite = happyFace;
                _interactiveData = happyAlien;
            }
        }
    }
}
