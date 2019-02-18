using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimals : MonoBehaviour
{
    public GameObject animal;
    public int animalCount = 20;

    // Spawn animalCount animals at random positions
    public void Spawn()
    {
        for (int a = 0; a < animalCount; a++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-20, 20), 0f, Random.Range(-20, 20));
            GameObject animalClone = Instantiate(animal, randomPosition, Quaternion.identity);
            animalClone.transform.parent = transform;
        }
    }

    // Kill all children
    public void Despawn()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
