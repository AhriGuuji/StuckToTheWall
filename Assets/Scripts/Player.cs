using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private InputAction _inputMovement;
    private Vector2 _currentMove;
    private InputAction _inputLook;
    private Vector2 _currentLook;
    private Rigidbody _rb;
    private Camera _cam;

    [Header("Movement and Physics")]
    [SerializeField]
    private float _movVel;
    [SerializeField]
    private float _rotVel;
    [SerializeField]
    private float _wallRotVel;
    [SerializeField]
    private float _limY;
    [SerializeField]
    private float _gravity = 9.81f;

    [Header("Raycast Settings")]
    [SerializeField]
    private float rayCastLenght = 1;
    [SerializeField]
    private float _raycastDownwardMultiplier = 2;
    [SerializeField]
    private LayerMask layerMask;
    private Vector3 _wallNormal;

    private void Awake()
    {
        _inputMovement = InputSystem.actions.FindAction("Move");
        _inputLook = InputSystem.actions.FindAction("Look");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _cam = GetComponentInChildren<Camera>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        _currentMove = _inputMovement.ReadValue<Vector2>();
        _currentLook = _inputLook.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        MoveForward();
        LookX();
        LookY();
    }

    private void MoveForward()
    {
        Vector3 move = (transform.forward
            * _currentMove.y * _movVel * Time.fixedDeltaTime) + (transform.right
            * _currentMove.x * _movVel * Time.fixedDeltaTime);

        _rb.MovePosition(_rb.position + move);
    }

    private void LookX()
    {
        float rotateAmount = _currentLook.x * _rotVel * Time.fixedDeltaTime;
        Quaternion faceDir = Quaternion.Euler(0, rotateAmount, 0);
        _rb.MoveRotation(_rb.rotation * faceDir);
    }

    private void LookY()
    {
        float rotateAmount = -_currentLook.y * _rotVel * Time.fixedDeltaTime;
        Vector3 rotation = new(rotateAmount, 0, 0);
        _cam.transform.Rotate(rotation, Space.Self);
    }

    private void ApplyGravity()
    {
        RaycastHit _hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.forward + -transform.up * _raycastDownwardMultiplier,
            out _hit, rayCastLenght, layerMask))
        {
            _wallNormal = _hit.transform.gameObject.GetComponent<SetNormal>().Normal;

            StartCoroutine(SwitchWall());

            _rb.AddForce(-transform.up * _gravity * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else _rb.AddForce(-transform.up * _gravity * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    private IEnumerator SwitchWall()
    {
        while (_wallNormal != transform.up.normalized)
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, _wallNormal) * transform.rotation;
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, _wallRotVel * Time.fixedDeltaTime);
            transform.rotation = newRotation;

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, (transform.forward + -transform.up * _raycastDownwardMultiplier) * rayCastLenght);
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
