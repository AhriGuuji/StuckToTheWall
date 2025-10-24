using System.Collections;
using UnityEngine;

public class WallRotation : MonoBehaviour
{
    [SerializeField]
    private float _rotateAngle;
    [SerializeField]
    private float _rotateSpeed = 10.0f;
    private Player _player;
    private Map _map;

    private void OnTriggerEnter(Collider other)
    {
        _player = other.GetComponent<Player>();
        _map = GetComponentInParent<Map>();
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        if (_player)
            StartCoroutine(WaitTillRotates(_map.gameObject, _player.transform.position, _rotateAngle));    
    }
    
    private IEnumerator WaitTillRotates(GameObject poll, Vector3 pivotPos,float degree)
    {
        Vector3 eulerDegree = poll.transform.eulerAngles;
        float finalDegree = eulerDegree.x + degree;
        float rotationProgress = 0f;
        
        Debug.Log(finalDegree);
        
        while (rotationProgress < 1f)
    {
        rotationProgress += Time.deltaTime * 2f; // 2f = 1/0.5f (inverse of duration)
        rotationProgress = Mathf.Clamp01(rotationProgress);
        
        // Calculate how much to rotate this frame
        float rotationThisFrame = degree * Time.deltaTime * 2f;
        poll.transform.RotateAround(pivotPos, Vector3.right, rotationThisFrame);
        
        yield return null;
    }
    
    // Snap to exact final rotation
    poll.transform.rotation = Quaternion.Euler(finalDegree, eulerDegree.y, eulerDegree.z);
}
}
