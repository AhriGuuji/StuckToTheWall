using UnityEngine;
using UnityEngine.UI;

namespace Tech.Scripts.Objects
{
    public class InteractiveAlien : Interactive
    {
        [Header("Alien Settings")] [SerializeField]
        private GameObject otherAlien;
        public override void DoSomething()
        {
            if (otherAlien) otherAlien.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
