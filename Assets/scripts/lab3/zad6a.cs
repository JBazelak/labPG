using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f; 


    private Vector3 velocity = Vector3.zero;  

    void Update()
    {

        Vector3 newPosition = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
        transform.position = newPosition;
    }
}
