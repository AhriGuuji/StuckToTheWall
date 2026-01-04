using System.Collections;
using UnityEngine;

namespace Tech.Scripts.Objects
{
    public class InteractiveConnector : Interactive
    {
        [Header("Interactive Connector Settings")]
        [SerializeField] private bool isConnector = true;
        [SerializeField] private int connectorID = 0;

        private bool _isWellConnected = false;
        private LineRenderer _line;

        public bool IsConnector => isConnector;
        public int ID => connectorID;
        
        public override void DoSomething()
        {
            Debug.Log(_interactionManager?.lastConnector?.ID);
            Debug.Log(_isWellConnected);

            if (isConnector)
                if (gameObject.TryGetComponent(out LineRenderer line))
                {
                    _line = line;

                    _line.SetPosition(0, transform.position);
                    _line.SetPosition(1, transform.position);

                    StartCoroutine(Connecting());
                }

            if(!isConnector && _interactionManager.lastConnector.IsConnector)
            {
                _interactionManager.lastConnector.GetComponent<LineRenderer>().SetPosition(1,transform.position);
                if (_interactionManager.lastConnector.ID == connectorID)
                    _isWellConnected=true;
                
                Debug.Log("Tried");
                _interactionManager.lastConnector = this;
            } 

            base.DoSomething();
        }

        protected override bool PuzzleFinished()
        {
            return _isWellConnected;
        }

        private IEnumerator Connecting()
        {
            bool running = true;

            while(running)
            {
                if(_interactionManager.InputInteract.WasPressedThisFrame() || _interactionManager.InputDrop.WasPressedThisFrame())
                    running = false;

                _line.SetPosition(1,Input.mousePosition);
                
                Debug.Log("Connecting");

                yield return null;
            }

            _interactionManager.lastConnector = this;
        }
    }
}
