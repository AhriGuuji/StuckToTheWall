using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RotateTriggerManager : MonoBehaviour
{
    [SerializeField]
    private WallRotation _trigger1;
    [SerializeField]
    private WallRotation _trigger2;
    [SerializeField]
    private float _cooldown = 1.0f;
    [SerializeField]
    private bool _startOn = false;
    private Player _player;

    private void Start()
    {
        _trigger1.GetComponent<BoxCollider>().enabled = _startOn;
        _trigger2.GetComponent<BoxCollider>().enabled = _startOn;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _player = collision.gameObject.GetComponent<Player>();

        if (_player != null)
        {
            StartCoroutine(OnRotation());
        }

        _player = null;
    }

    private IEnumerator OnRotation()
    {
        YieldInstruction wfs = new WaitForSeconds(_cooldown);

        yield return wfs;

        _trigger1.GetComponent<BoxCollider>().enabled = true;
        _trigger2.GetComponent<BoxCollider>().enabled = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        _player = collision.gameObject.GetComponent<Player>();

        if (_player != null)
        {
            _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            _trigger1.GetComponent<BoxCollider>().enabled = false;
            _trigger2.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("out");
        }

        _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None 
            | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _player = null;
    }
}
