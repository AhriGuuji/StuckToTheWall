using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Vector2             _currentMove;
    private Vector2             _currentLook;
    private Rigidbody           _rb;
    private Camera              _cam;
    private Vector3             _camRotation;
    private InteractionManager  _manager;
    public Camera               Cam => _cam;

    [Header("Movement and Physics")]
    [SerializeField]
    private float               movVel;
    [SerializeField]
    private float               rotVel;
    [SerializeField]
    private float               wallRotVel;
    [SerializeField]
    private float               limYUp;
    [SerializeField]
    private float               limYDown;
    [SerializeField]
    private float               gravity = 9.81f;

    [Header("Raycast Settings")]
    [SerializeField]
    private float               raycastLenght = 1;
    [SerializeField]
    private float               raycastDownwardMultiplier = 2;
    [SerializeField]
    private LayerMask           mask;
    private Vector3             _wallNormal;

    private void Awake()
    {
        _manager = InteractionManager.instance;
        _cam = GetComponentInChildren<Camera>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        _currentMove = _manager.InputMovement.ReadValue<Vector2>();
        _currentLook = _manager.InputLook.ReadValue<Vector2>();
        
        if(_manager.InputEscape.WasPressedThisFrame())
            Application.Quit();
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
            * _currentMove.y * movVel * Time.fixedDeltaTime) + (transform.right
            * _currentMove.x * movVel * Time.fixedDeltaTime);

        _rb.MovePosition(_rb.position + move);
    }

    private void LookX()
    {
        float rotateAmount = _currentLook.x * rotVel * Time.fixedDeltaTime;
        Quaternion faceDir = Quaternion.Euler(0, rotateAmount, 0);
        _rb.MoveRotation(_rb.rotation * faceDir);
    }

    private void LookY()
    {
        _camRotation = _cam.transform.localEulerAngles;

        _camRotation.x -= _currentLook.y;

        if (_camRotation.x > 180f)
            _camRotation.x = Mathf.Max(limYUp, _camRotation.x);
        else
            _camRotation.x = Mathf.Min(limYDown, _camRotation.x);

        _cam.transform.localEulerAngles = _camRotation;
    }

    private void ApplyGravity()
    {
        RaycastHit _hit;

        if (Physics.Raycast(transform.position, transform.forward + -transform.up * raycastDownwardMultiplier,
            out _hit, raycastLenght, mask))
        {
            _wallNormal = _hit.transform.gameObject.GetComponent<SetNormal>().Normal;

            StartCoroutine(SwitchWall());

            _rb.AddForce(-transform.up * (gravity * Time.fixedDeltaTime), ForceMode.Acceleration);
        }
        else _rb.AddForce(-transform.up * (gravity * Time.fixedDeltaTime), ForceMode.Acceleration);
    }

    private IEnumerator SwitchWall()
    {
        while (_wallNormal != transform.up.normalized)
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, _wallNormal) * transform.rotation;
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, wallRotVel * Time.fixedDeltaTime);
            transform.rotation = newRotation;

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, (transform.forward + -transform.up * raycastDownwardMultiplier) * raycastLenght);
    }
}
