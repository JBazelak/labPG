using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float speed = 2.0f;
    private float targetPosition = 10f;
    private Vector3 startPosition;
    private bool movingForward;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (movingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition + new Vector3(targetPosition, 0, 0), speed * Time.deltaTime);
            if (transform.position.x >= startPosition.x + targetPosition)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

            if (transform.position.x <= startPosition.x)
            {
                movingForward = true;
            }
        }
    }
}