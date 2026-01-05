using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Tech.Scripts.Objects
{
    public class InteractivePlacer : Interactive
    {
        [Header("Additional Settings")] [SerializeField]
        private GameObject objectToPlace;
        [SerializeField]
        private Puzzle goal;
        private Interactive _objectData;
        public bool IsPlaced {get; private set;}
        public GameObject objectPlaced => objectToPlace;

        private void Start()
        {
            _objectData = objectToPlace.GetComponent<Interactive>();
        }

        public override void DoSomething()
        {
            if (_interactionManager.playerInventory.IsSelected(_objectData) && _objectData.IsDone)
            {
                objectToPlace.transform.position = transform.position;
                objectToPlace.SetActive(true);
                IsPlaced = true;
                _interactionManager.playerInventory.Remove(_objectData);
            }
            
            base.DoSomething();
            //GetComponent<BoxCollider>().enabled = false;
        }

        protected override bool PuzzleFinished()
        {
            return IsPlaced && goal.IsComplete;
        }
    }
}
