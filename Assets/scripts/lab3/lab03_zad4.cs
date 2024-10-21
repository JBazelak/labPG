using UnityEngine;

public class PlayerMovementSimple : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 360f;

    void Update()
    {
        // Pobierz dane wej�ciowe gracza
        float moveForward = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        // Poruszanie si� w prz�d/ty�
        transform.Translate(Vector3.forward * moveForward * moveSpeed * Time.deltaTime);

        // Obracanie obiektu wok� osi Y
        transform.Rotate(Vector3.up, turn * turnSpeed * Time.deltaTime);
    }
}
