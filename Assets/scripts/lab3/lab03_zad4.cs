using UnityEngine;

public class PlayerMovementSimple : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 360f;

    void Update()
    {
        // Pobierz dane wejœciowe gracza
        float moveForward = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        // Poruszanie siê w przód/ty³
        transform.Translate(Vector3.forward * moveForward * moveSpeed * Time.deltaTime);

        // Obracanie obiektu wokó³ osi Y
        transform.Rotate(Vector3.up, turn * turnSpeed * Time.deltaTime);
    }
}
