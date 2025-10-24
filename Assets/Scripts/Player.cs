using System.Collections;
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
    [SerializeField]
    private float _gravity = 9.81f;
    [SerializeField]
    private float rayCastLenght = 1;
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

        _rb.MovePosition(_rb.position + -transform.up * _gravity * Time.fixedDeltaTime);
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
            * _currentMove.y * _moveVelocity * Time.fixedDeltaTime) + (transform.right
            * _currentMove.x * _moveVelocity * Time.fixedDeltaTime);

        _rb.MovePosition(_rb.position + move);
    }

    private void LookX()
    {
        float rotateAmount = _currentLook.x * _rotationVelocity * Time.fixedDeltaTime;
        Quaternion faceDir = Quaternion.Euler(0, rotateAmount, 0);
        _rb.MoveRotation(_rb.rotation * faceDir);
    }

    private void LookY()
    {
        float rotateAmount = -_currentLook.y * _rotationVelocity * Time.fixedDeltaTime;
        Vector3 rotation = new(rotateAmount, 0, 0);
        _cam.transform.Rotate(rotation, Space.Self);
    }

    private void ApplyGravity()
    {
        RaycastHit _hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.forward,
            out _hit, rayCastLenght, layerMask))
        {
            //StartCoroutine(JumpWall());

            _wallNormal = _hit.transform.gameObject.GetComponent<SetNormal>().Normal;
            float rotation = Mathf.Clamp(-Vector3.Angle(transform.up, _wallNormal), -90, 90);
            Quaternion quaternion = Quaternion.Euler(new(rotation,0,0));
            _rb.MoveRotation(_rb.rotation*quaternion);
            _rb.AddForce(-transform.up * _gravity * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else _rb.AddForce(-transform.up * _gravity * Time.fixedDeltaTime, ForceMode.Acceleration);

        //_hit = Physics.Raycast(transform.position, -transform.up, rayCastLenght);
    }
    
    /*private IEnumerator JumpWall()
    {
        while(true)
        {
            _controller.Move(transform.up*Time.deltaTime);

            yield return new WaitForSeconds(1);

            yield break;
        }
    }*/

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
