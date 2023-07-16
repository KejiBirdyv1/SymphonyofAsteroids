using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform Asteroid;

    public void SpawnAsteroid()
    {
        Instantiate(Asteroid, transform.position, Quaternion.identity);
    }
}
