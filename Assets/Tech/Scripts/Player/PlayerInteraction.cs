using Tech.Scripts.Map;
using Tech.Scripts.Objects;
using UnityEngine;

namespace Tech.Scripts.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private UIManager  _uiManager;
        [SerializeField] private float      _maxInteractionDistance;
        public Interactive GetCurrentInteractive => _currentInteractive;
        private Transform   _cameraTransform;
        private Interactive _currentInteractive;
        private bool _refreshCurrentInteractive;
        private bool _canDropHere;
        private InteractionManager _manager;

        void Start()
        {
            _cameraTransform            = GetComponentInChildren<Camera>().transform;
            _currentInteractive         = null;
            _refreshCurrentInteractive  = false;
            _manager                    = InteractionManager.instance;
        }

        void Update()
        {
            UpdateCurrentInteractive();
            CheckForPlayerInteraction();
        }

        private void UpdateCurrentInteractive()
        {
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out RaycastHit hitInfo, _maxInteractionDistance))
                CheckObjectForInteraction(hitInfo.collider);
            else if (_currentInteractive != null)
                ClearCurrentInteractive();
            else if (_canDropHere)
                _canDropHere = false;
        }

        private void CheckObjectForInteraction(Collider collider)
        {
            Interactive interactive = collider.GetComponent<Interactive>();
            SetNormal wall = collider.GetComponent<SetNormal>();

            if (wall != null)
                _canDropHere = true;
            else
                _canDropHere = false;
        
            if (interactive == null || !interactive.isOn)
            {
                if (_currentInteractive != null)
                    ClearCurrentInteractive();
            }
            else if (interactive != _currentInteractive || _refreshCurrentInteractive)
                SetCurrentInteractive(interactive);
        }

        private void ClearCurrentInteractive()
        {
            if (_currentInteractive.OutlineMesh) _currentInteractive.OutlineMesh.SetActive(false);
        
            _currentInteractive = null;
            _uiManager.ShowDefaultCrosshair();
            _uiManager.HideInteractionPanel();
        }

        private void SetCurrentInteractive(Interactive interactive)
        {
            _currentInteractive         = interactive;
            _refreshCurrentInteractive  = false;
        
            if (_currentInteractive.OutlineMesh) _currentInteractive.OutlineMesh.SetActive(true);

            string interactionMessage = interactive.GetInteractionMessage();

            if (interactionMessage != null && interactionMessage.Length > 0)
            {
                _uiManager.ShowInteractionCrosshair();
                _uiManager.ShowInteractionPanel(interactionMessage);
            }
            else
            {
                _uiManager.ShowDefaultCrosshair();
                _uiManager.HideInteractionPanel();
            }
        }

        private void CheckForPlayerInteraction()
        {
            if (_manager.InputInteract.WasPressedThisFrame() 
                && _currentInteractive != null 
                && !_currentInteractive.doingPuzzle)
            {
                _currentInteractive.Interact();
                _refreshCurrentInteractive = true;
            }

            if (_manager.InputDrop.WasPressedThisFrame() 
                && _currentInteractive != null)
            {
                if (_currentInteractive.doingPuzzle)
                    _currentInteractive.Leave();
            }
        
            if (_manager.InputDrop.WasPressedThisFrame() 
                && _canDropHere && _manager.playerInventory.GetSelected())
                _manager.playerInventory.GetSelected().Drop();
        }

        public void RefreshCurrentInteractive()
        {
            _refreshCurrentInteractive = true;
        }
    }
}
