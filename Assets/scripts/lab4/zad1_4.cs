using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomCubesGenerator : MonoBehaviour
{
    List<Vector3> positions = new List<Vector3>();
    public float delay = 3.0f;
    public int objectCount = 10;
    public GameObject block; 
    public Material[] materials; 
    private Renderer blockRenderer;

    private Bounds platformBounds;

    void Start()
    {
        platformBounds = GetComponent<Renderer>().bounds;

        GenerateRandomPositions();
        StartCoroutine(GenerujObiekt());
    }

    void GenerateRandomPositions()
    {
        List<int> pozycje_x = new List<int>(Enumerable.Range((int)platformBounds.min.x, (int)(platformBounds.size.x)).OrderBy(x => Guid.NewGuid()).Take(objectCount));
        List<int> pozycje_z = new List<int>(Enumerable.Range((int)platformBounds.min.z, (int)(platformBounds.size.z)).OrderBy(x => Guid.NewGuid()).Take(objectCount));

        for (int i = 0; i < objectCount; i++)
        {
            float randomX = UnityEngine.Random.Range(platformBounds.min.x, platformBounds.max.x);
            float randomZ = UnityEngine.Random.Range(platformBounds.min.z, platformBounds.max.z);
            this.positions.Add(new Vector3(randomX, platformBounds.max.y + 1, randomZ)); 
        }
    }

    IEnumerator GenerujObiekt()
    {
        Debug.Log("Wywo³ano coroutine");
        foreach (Vector3 pos in positions)
        {
            GameObject newBlock = Instantiate(this.block, pos, Quaternion.identity);

            blockRenderer = newBlock.GetComponent<Renderer>();
            if (materials.Length > 0)
            {
                Material randomMaterial = materials[UnityEngine.Random.Range(0, materials.Length)];
                blockRenderer.material = randomMaterial;
            }

            yield return new WaitForSeconds(this.delay);
        }
        StopCoroutine(GenerujObiekt());
    }
}
