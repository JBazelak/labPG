using UnityEngine;

public class MoveCubeInSquare : MonoBehaviour
{
    public float speed = 2.0f;
    public float distanceToMove = 10f;  // Odleg�o��, kt�r� Cube przebywa przed obrotem
    private Vector3 startPosition;       // Pozycja startowa Cube
    private Vector3 targetPosition;      // Cel, do kt�rego Cube si� porusza
    private Quaternion targetRotation;   // Docelowa rotacja Cube
    private bool isMoving = true;        // Czy Cube porusza si�
    private bool isRotating = false;     // Czy Cube si� obraca

    void Start()
    {
        // Ustawiamy pozycj� startow� i cel dla pierwszego ruchu
        startPosition = transform.position;
        targetPosition = startPosition + transform.forward * distanceToMove;
        targetRotation = transform.rotation; // Pocz�tkowa rotacja
    }

    void Update()
    {
        if (isMoving)
        {
            // Ruch w kierunku celu
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Sprawdzanie, czy Cube dotar� do celu
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;    // Zatrzymanie ruchu
                isRotating = true;   // Przygotowanie do obrotu
                targetRotation *= Quaternion.Euler(0, 90, 0); // Ustawienie nowej docelowej rotacji (90 stopni w prawo)
            }
        }

        if (isRotating)
        {
            // Obr�t Cube'a w prawo o 90 stopni
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * 100 * Time.deltaTime);

            // Sprawdzenie, czy obr�t zosta� zako�czony
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation; // Upewnij si�, �e rotacja jest dok�adna
                isRotating = false;   // Zatrzymanie obrotu
                isMoving = true;      // Ponowne rozpocz�cie ruchu
                startPosition = transform.position; // Nowa pozycja startowa po obrocie
                targetPosition = startPosition + transform.forward * distanceToMove; // Nowy cel po obrocie
            }
        }
    }
}
