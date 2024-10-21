using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;  // Prefab Cube'a
    public GameObject planeObject; // Obiekt p³aszczyzny
    public int numberOfCubes = 10; // Liczba Cube'ów do wygenerowania
    public Vector2 planeSize = new Vector2(10, 10); // Rozmiar p³aszczyzny
    public float respawnTime = 3f; // Czas po którym Cube'y znikaj¹ i pojawiaj¹ siê nowe

    private List<Vector3> usedPositions = new List<Vector3>(); // Lista przechowuj¹ca u¿yte pozycje
    private List<GameObject> spawnedCubes = new List<GameObject>(); // Lista przechowuj¹ca wygenerowane Cube'y

    void Start()
    {
        StartCoroutine(RespawnCubes());
    }

    // Funkcja generuj¹ca Cube'y
    void GenerateCubes()
    {
        // Wyczyszczenie list u¿ywanych pozycji i wygenerowanych Cube'ów
        usedPositions.Clear();

        // Generowanie Cube'ów
        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject cube = Instantiate(cubePrefab, randomPosition, Quaternion.identity);
            spawnedCubes.Add(cube); // Dodaj Cube'a do listy
        }
    }

    // Funkcja do usuwania Cube'ów
    void RemoveCubes()
    {
        foreach (GameObject cube in spawnedCubes)
        {
            Destroy(cube); // Usuniêcie Cube'a
        }
        spawnedCubes.Clear(); // Wyczyœæ listê Cube'ów
    }

    // Funkcja, która co 3 sekundy respawnuje Cube'y
    IEnumerator RespawnCubes()
    {
        while (true)
        {
            GenerateCubes(); // Generowanie nowych Cube'ów
            yield return new WaitForSeconds(respawnTime); // Czekaj 3 sekundy
            RemoveCubes(); // Usuniêcie Cube'ów
        }
    }

    // Funkcja do losowania pozycji, upewniaj¹c siê, ¿e Cube'y nie wychodz¹ poza granice i nie nachodz¹ na siebie
    Vector3 GetRandomPosition()
    {
        Vector3 position;
        bool isPositionValid;

        do
        {
            isPositionValid = true;

            // Losuj pozycjê na p³aszczyŸnie o rozmiarach 10x10
            float x = Random.Range(-planeSize.x / 2f, planeSize.x / 2f);
            float z = Random.Range(-planeSize.y / 2f, planeSize.y / 2f);

            // Zaokr¹glij pozycjê, aby upewniæ siê, ¿e Cube'y nie s¹ za blisko siebie
            position = new Vector3(Mathf.Round(x), 0f, Mathf.Round(z));

            // Przesuñ pozycjê wzglêdem obiektu p³aszczyzny
            position += planeObject.transform.position;

            // SprawdŸ, czy pozycja ju¿ nie jest zajêta
            foreach (Vector3 usedPosition in usedPositions)
            {
                if (Vector3.Distance(usedPosition, position) < 1f) // Minimalna odleg³oœæ miêdzy Cube'ami
                {
                    isPositionValid = false;
                    break;
                }
            }
        }
        while (!isPositionValid); // Powtarzaj, dopóki nie znajdziesz wolnej pozycji

        usedPositions.Add(position); // Dodaj now¹ pozycjê do listy zajêtych pozycji
        return position;
    }
}
