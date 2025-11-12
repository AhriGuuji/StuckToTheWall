using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private UIManager  _uiManager;
    [SerializeField] private float      _maxInteractionDistance;
    [SerializeField] private string     _interactInput = "Interact";
    [SerializeField] private string     _dropInput = "Drop";
    private Transform   _cameraTransform;
    private Interactive _currentInteractive;
    private bool _refreshCurrentInteractive;
    private InputAction _interact;
    private InputAction _drop;

    void Start()
    {
        _cameraTransform            = GetComponentInChildren<Camera>().transform;
        _currentInteractive         = null;
        _refreshCurrentInteractive = false;
        _interact = InputSystem.actions.FindAction(_interactInput);
        _drop = InputSystem.actions.FindAction(_dropInput);
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
    }

    private void CheckObjectForInteraction(Collider collider)
    {
        Interactive interactive = collider.GetComponent<Interactive>();

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
        _currentInteractive = null;
        _uiManager.ShowDefaultCrosshair();
        _uiManager.HideInteractionPanel();
    }

    private void SetCurrentInteractive(Interactive interactive)
    {
        _currentInteractive         = interactive;
        _refreshCurrentInteractive  = false;

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
        if (_interact.WasPressedThisFrame() 
            && _currentInteractive != null 
            && !_currentInteractive.doingPuzzle)
        {
            _currentInteractive.Interact();
            _refreshCurrentInteractive = true;
        }

        if (_drop.WasPressedThisFrame() 
            && _currentInteractive != null)
        {
            if (_currentInteractive.doingPuzzle)
                _currentInteractive.Leave();
        }
    }

    public void RefreshCurrentInteractive()
    {
        _refreshCurrentInteractive = true;
    }
}
