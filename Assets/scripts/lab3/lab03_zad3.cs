using UnityEngine;

public class MoveCubeInSquare : MonoBehaviour
{
    public float speed = 2.0f;
    public float distanceToMove = 10f;  // Odleg³oœæ, któr¹ Cube przebywa przed obrotem
    private Vector3 startPosition;       // Pozycja startowa Cube
    private Vector3 targetPosition;      // Cel, do którego Cube siê porusza
    private Quaternion targetRotation;   // Docelowa rotacja Cube
    private bool isMoving = true;        // Czy Cube porusza siê
    private bool isRotating = false;     // Czy Cube siê obraca

    void Start()
    {
        // Ustawiamy pozycjê startow¹ i cel dla pierwszego ruchu
        startPosition = transform.position;
        targetPosition = startPosition + transform.forward * distanceToMove;
        targetRotation = transform.rotation; // Pocz¹tkowa rotacja
    }

    void Update()
    {
        if (isMoving)
        {
            // Ruch w kierunku celu
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Sprawdzanie, czy Cube dotar³ do celu
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;    // Zatrzymanie ruchu
                isRotating = true;   // Przygotowanie do obrotu
                targetRotation *= Quaternion.Euler(0, 90, 0); // Ustawienie nowej docelowej rotacji (90 stopni w prawo)
            }
        }

        if (isRotating)
        {
            // Obrót Cube'a w prawo o 90 stopni
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * 100 * Time.deltaTime);

            // Sprawdzenie, czy obrót zosta³ zakoñczony
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation; // Upewnij siê, ¿e rotacja jest dok³adna
                isRotating = false;   // Zatrzymanie obrotu
                isMoving = true;      // Ponowne rozpoczêcie ruchu
                startPosition = transform.position; // Nowa pozycja startowa po obrocie
                targetPosition = startPosition + transform.forward * distanceToMove; // Nowy cel po obrocie
            }
        }
    }
}
