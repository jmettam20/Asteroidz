using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidSpawner : MonoBehaviour
{

    public asteroidScript astaroidPrefab; 
    public float spawnRate = 2.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15;
    public float trajectoryVarience = 15.0f; 

    private void Start() {
        InvokeRepeating(nameof(Spawn),this.spawnRate,this.spawnRate); //spawn asteroid prefab exery x time
    }

    private void Spawn() {

        for (int i = 0; i < this.spawnAmount; i++) {//spawn asteroids + amount 

            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance; //spawn going in direction 

            Vector3 spawnPoint = this.transform.position + spawnDirection;//set spawn point 

            float varience = Random.Range(this.trajectoryVarience, this.trajectoryVarience); //vary trajectory
            Quaternion rotation = Quaternion.AngleAxis(varience,Vector3.forward);//rotate

            asteroidScript asteroid = Instantiate(this.astaroidPrefab, spawnPoint, rotation);//spawn asteroid 
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);//vary size
            asteroid.SetTrajectory(rotation * -spawnDirection);//set trajectory + rotation 
        }
    }
}
