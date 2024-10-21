using UnityEngine;

public class ObjectFollowerB : MonoBehaviour
{
    public Transform target; 
    public float lerpSpeed = 5f;   

    private Vector3 velocity = Vector3.zero; 

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, lerpSpeed * Time.deltaTime);
    }
}
