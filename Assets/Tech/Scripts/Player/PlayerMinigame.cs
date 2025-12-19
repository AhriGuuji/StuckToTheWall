using Tech.Scripts.Objects;
using UnityEngine;

public class PlayerMinigame : MonoBehaviour
{
    [SerializeField] private UIManager  uiManager;
    [SerializeField] private LayerMask puzzleLayerMask;
    private Vector3 _mousePos;
    private Ray _ray;
    private bool _hit;
    private InteractionManager _manager;
    private Camera _cam;

    private void Awake()
    {
        _manager = InteractionManager.instance;
        _cam =  Camera.main;
    }

    private void OnEnable()
    {
        uiManager.ShowCursor();
    }

    private void OnDisable()
    {
        uiManager.HideCursor();
    }

    private void Update()
    {
        _mousePos = Input.mousePosition;
        _ray = _cam.ScreenPointToRay(_mousePos);
        _hit = Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity, puzzleLayerMask,  QueryTriggerInteraction.Collide );

        Debug.DrawRay(_ray.origin, _ray.direction * 100, Color.red);
        
        if (_hit)
            if (_manager.InputInteract.WasPressedThisFrame())
            {
                hit.transform.GetComponent<InteractivePiece>().DoSomething();
            }
    }
}
