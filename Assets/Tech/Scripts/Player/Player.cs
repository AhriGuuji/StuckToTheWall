using System.Collections;
using Tech.Scripts.Map;
using UnityEngine;

namespace Tech.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        private Vector2             _currentMove;
        private Vector2             _currentLook;
        private Rigidbody           _rb;
        private Camera              _cam;
        private Vector3             _camRotation;
        private InteractionManager  _manager;

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
        private bool _rotating;

        private void Awake()
        {
            _manager = InteractionManager.instance;
            _cam = GetComponentInChildren<Camera>();
            _rb = GetComponent<Rigidbody>();
            _rotating = false;
        }
        private void Update()
        {
            _currentMove = _manager.InputMovement.ReadValue<Vector2>();
            _currentLook = _manager.InputLook.ReadValue<Vector2>();
        
            LookX();
            LookY();
        
            if(_manager.InputEscape.WasPressedThisFrame())
                Application.Quit();
        }

        private void FixedUpdate()
        {
            ApplyGravity();
            MoveForward();
        }

        private void MoveForward()
        {
            Vector3 move = (transform.forward * (_currentMove.y * movVel * Time.fixedDeltaTime)) 
                           + (transform.right * (_currentMove.x * movVel * Time.fixedDeltaTime));

            _rb.MovePosition(_rb.position + move);
        }

        private void LookX()
        {
            float rotateAmount = _currentLook.x * rotVel * Time.deltaTime;
            Vector3 faceDir = new(0, rotateAmount, 0);
            transform.Rotate(faceDir);
        }

        private void LookY()
        {
            _camRotation = _cam.transform.localEulerAngles;

            _camRotation.x -= _currentLook.y * rotVel * Time.deltaTime;

            _camRotation.x = _camRotation.x > 180f ? 
                _camRotation.x = Mathf.Max(limYUp, _camRotation.x) : 
                _camRotation.x = Mathf.Min(limYDown, _camRotation.x);

            _cam.transform.localEulerAngles = _camRotation;
        }

        private void ApplyGravity()
        {
            RaycastHit _hit;

            if (!_rotating && Physics.Raycast(transform.position, transform.forward + -transform.up * raycastDownwardMultiplier,
                    out _hit, raycastLenght, mask))
            {
                _wallNormal = _hit.transform.gameObject.GetComponent<SetNormal>().Normal;
            
                if (_wallNormal != transform.up.normalized)
                    StartCoroutine(SwitchWall());
            }
        
            _rb.AddForce(-transform.up * gravity, ForceMode.Acceleration);
        }

        private IEnumerator SwitchWall()
        {
            _rotating = true;
            
            while ((_wallNormal - transform.up.normalized).magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.FromToRotation(transform.up, _wallNormal) * transform.rotation;
                Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, wallRotVel);
                transform.rotation = newRotation;
                yield return null;
            }
            
            transform.rotation = Quaternion.FromToRotation(transform.up, _wallNormal) * transform.rotation;
            _rotating = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, (transform.forward + -transform.up * raycastDownwardMultiplier) * raycastLenght);
        }
    }
}
