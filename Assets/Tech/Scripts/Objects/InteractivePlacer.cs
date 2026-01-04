using UnityEngine;
using UnityEngine.UI;

namespace Tech.Scripts.Objects
{
    public class InteractivePlacer : Interactive
    {
        [Header("Additional Settings")] [SerializeField]
        private GameObject objectToPlace;
        private Interactive _objectData;
        public bool IsPlaced {get; private set;}

        private void Start()
        {
            _objectData = objectToPlace.GetComponent<Interactive>();
        }

        public override void DoSomething()
        {
            if (_interactionManager.playerInventory.IsSelected(_objectData))
            {
                objectToPlace.transform.position = transform.position;
                objectToPlace.SetActive(true);
                IsPlaced = true;
                _interactionManager.playerInventory.Remove(_objectData);
            }
            
            //GetComponent<BoxCollider>().enabled = false;
        }
    }
}
