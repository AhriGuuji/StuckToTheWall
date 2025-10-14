using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    private InputAction _inputMovement;
    private Vector2 _currentMove;
    private InputAction _inputLook;
    private Vector2 _currentLook;

    [SerializeField]
    private Rigidbody _rb;
    private Camera _cam;

    [Header("Values")]
    [SerializeField]
    private float _moveVelocity;
    [SerializeField]
    private float _rotationVelocity;

    void Awake()
    {
        _inputMovement = InputSystem.actions.FindAction("Move");
        _inputLook = InputSystem.actions.FindAction("Look");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _cam = GetComponentInChildren<Camera>();
    }
    void Update()
    {
        _currentMove = _inputMovement.ReadValue<Vector2>();
        _currentLook = _inputLook.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        MoveForward();
        LookX();
        LookY();
    }

    private void MoveForward()
    {
        Vector3 kdnaokndao = (transform.forward
            * _currentMove.y * _moveVelocity * Time.fixedDeltaTime) + (transform.right
            * _currentMove.x * _moveVelocity * Time.fixedDeltaTime);

        _rb.MovePosition(_rb.position + kdnaokndao);
    }

    private void LookX()
    {
        float rotateAmount = _currentLook.x * _rotationVelocity * Time.deltaTime;
        Quaternion quaternion = Quaternion.Euler(0, rotateAmount, 0);
        _rb.MoveRotation(_rb.rotation * quaternion);
    }

    private void LookY()
    {
        float rotateAmount = -_currentLook.y * _rotationVelocity * Time.deltaTime;
        Vector3 rotation = new (rotateAmount, 0, 0);
        _cam.transform.Rotate(rotation, Space.Self);
    }

    private void OnEnable()
    {
        _inputMovement.Enable();
        _inputLook.Enable();
    }

    private void OnDisable()
    {
        _inputMovement.Disable();
        _inputLook.Disable();
    }
}
