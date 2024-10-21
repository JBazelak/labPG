using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;  // Prefab Cube'a
    public GameObject planeObject; // Obiekt p�aszczyzny
    public int numberOfCubes = 10; // Liczba Cube'�w do wygenerowania
    public Vector2 planeSize = new Vector2(10, 10); // Rozmiar p�aszczyzny
    public float respawnTime = 3f; // Czas po kt�rym Cube'y znikaj� i pojawiaj� si� nowe

    private List<Vector3> usedPositions = new List<Vector3>(); // Lista przechowuj�ca u�yte pozycje
    private List<GameObject> spawnedCubes = new List<GameObject>(); // Lista przechowuj�ca wygenerowane Cube'y

    void Start()
    {
        StartCoroutine(RespawnCubes());
    }

    // Funkcja generuj�ca Cube'y
    void GenerateCubes()
    {
        // Wyczyszczenie list u�ywanych pozycji i wygenerowanych Cube'�w
        usedPositions.Clear();

        // Generowanie Cube'�w
        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject cube = Instantiate(cubePrefab, randomPosition, Quaternion.identity);
            spawnedCubes.Add(cube); // Dodaj Cube'a do listy
        }
    }

    // Funkcja do usuwania Cube'�w
    void RemoveCubes()
    {
        foreach (GameObject cube in spawnedCubes)
        {
            Destroy(cube); // Usuni�cie Cube'a
        }
        spawnedCubes.Clear(); // Wyczy�� list� Cube'�w
    }

    // Funkcja, kt�ra co 3 sekundy respawnuje Cube'y
    IEnumerator RespawnCubes()
    {
        while (true)
        {
            GenerateCubes(); // Generowanie nowych Cube'�w
            yield return new WaitForSeconds(respawnTime); // Czekaj 3 sekundy
            RemoveCubes(); // Usuni�cie Cube'�w
        }
    }

    // Funkcja do losowania pozycji, upewniaj�c si�, �e Cube'y nie wychodz� poza granice i nie nachodz� na siebie
    Vector3 GetRandomPosition()
    {
        Vector3 position;
        bool isPositionValid;

        do
        {
            isPositionValid = true;

            // Losuj pozycj� na p�aszczy�nie o rozmiarach 10x10
            float x = Random.Range(-planeSize.x / 2f, planeSize.x / 2f);
            float z = Random.Range(-planeSize.y / 2f, planeSize.y / 2f);

            // Zaokr�glij pozycj�, aby upewni� si�, �e Cube'y nie s� za blisko siebie
            position = new Vector3(Mathf.Round(x), 0f, Mathf.Round(z));

            // Przesu� pozycj� wzgl�dem obiektu p�aszczyzny
            position += planeObject.transform.position;

            // Sprawd�, czy pozycja ju� nie jest zaj�ta
            foreach (Vector3 usedPosition in usedPositions)
            {
                if (Vector3.Distance(usedPosition, position) < 1f) // Minimalna odleg�o�� mi�dzy Cube'ami
                {
                    isPositionValid = false;
                    break;
                }
            }
        }
        while (!isPositionValid); // Powtarzaj, dop�ki nie znajdziesz wolnej pozycji

        usedPositions.Add(position); // Dodaj now� pozycj� do listy zaj�tych pozycji
        return position;
    }
}
