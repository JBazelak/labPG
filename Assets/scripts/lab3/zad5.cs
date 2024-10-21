using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;  
    public GameObject planeObject;
    public int numberOfCubes = 10;
    public Vector2 planeSize = new Vector2(10, 10); 
    public float respawnTime = 3f; 

    private List<Vector3> usedPositions = new List<Vector3>(); 
    private List<GameObject> spawnedCubes = new List<GameObject>(); 

    void Start()
    {
        StartCoroutine(RespawnCubes());
    }

    // Funkcja generuj�ca Cube'y
    void GenerateCubes()
    {
        usedPositions.Clear();

        // Generowanie Cube'�w
        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject cube = Instantiate(cubePrefab, randomPosition, Quaternion.identity);
            spawnedCubes.Add(cube); 
        }
    }


    void RemoveCubes()
    {
        foreach (GameObject cube in spawnedCubes)
        {
            Destroy(cube);
        }
        spawnedCubes.Clear(); 
    }

    // Funkcja, kt�ra co 3 sekundy respawnuje Cube'y
    IEnumerator RespawnCubes()
    {
        while (true)
        {
            GenerateCubes(); 
            yield return new WaitForSeconds(respawnTime); 
            RemoveCubes();
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

            float x = Random.Range(-planeSize.x / 2f, planeSize.x / 2f);
            float z = Random.Range(-planeSize.y / 2f, planeSize.y / 2f);

            // Zaokr�glij pozycj�, aby upewni� si�, �e Cube'y nie s� za blisko siebie
            position = new Vector3(Mathf.Round(x), 0f, Mathf.Round(z));


            position += planeObject.transform.position;

            foreach (Vector3 usedPosition in usedPositions)
            {
                if (Vector3.Distance(usedPosition, position) < 1f) 
                {
                    isPositionValid = false;
                    break;
                }
            }
        }
        while (!isPositionValid); 

        usedPositions.Add(position); 
        return position;
    }
}
