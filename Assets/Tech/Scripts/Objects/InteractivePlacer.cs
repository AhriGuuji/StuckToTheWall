using UnityEngine;
using UnityEngine.UI;

namespace Tech.Scripts.Objects
{
    public class InteractivePlacer : Interactive
    {
        [Header("Additional Settings")] [SerializeField]
        private GameObject objectToPlace;
        public bool IsPlaced {get; private set;}
        public override void DoSomething()
        {
            objectToPlace.transform.position = transform.position;
            objectToPlace.SetActive(true);
            IsPlaced = true;
        }
    }
}
