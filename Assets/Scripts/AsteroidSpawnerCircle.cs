using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class AsteroidSpawnerCircle : MonoBehaviour
{
    public float SpawnRadius = 5;
    public List<GameObject> AsteroidPrefabs;
    public float AsteroidSpeedMin = 1;
    public float AsteroidSpeedMax = 3;
    public float AsteroidSizeMin = 1;
    public float AsteroidSizeMax = 3;
    public float Frequency = 1.5f;
    public float SpawnAtATime = 4;

    private GameController gameController;
    private float lastSpawnTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad - lastSpawnTime >= Frequency)
        {
            var difficulty = Mathf.Clamp(Time.timeSinceLevelLoad / gameController.MaxDifficultyTime, 0, 1);
            var spawnCount = Mathf.Clamp(SpawnAtATime * difficulty, 1, SpawnAtATime);
            
            for (int i = 0; i < spawnCount; i++)
            {
                Spawn();
            }
            lastSpawnTime = Time.time;
        }
    }

    private void Spawn()
    {
        var angle = Random.Range(0, 360);
        var spawnPosition = GetSpawnPosition(angle);

        var asteroidRotation = Random.Range(0, 360);
        var id = Random.Range(0, AsteroidPrefabs.Count - 1);
        var asteroid = Instantiate(AsteroidPrefabs[id], spawnPosition, Quaternion.AngleAxis(asteroidRotation, Vector3.forward));
        var size = Random.Range(AsteroidSizeMin, AsteroidSizeMax);
        asteroid.transform.localScale = Vector3.one * size;
        
        var asteroidRb = asteroid.GetComponent<Rigidbody2D>();

        var asteroidSpeed = Random.Range(AsteroidSpeedMin, AsteroidSpeedMax);
        asteroidRb.velocity = GetAsteroidVelocity(angle) * asteroidSpeed;
    }

    private Vector2 GetSpawnPosition(float angle)
    {
        var y = Mathf.Cos(angle * Mathf.Deg2Rad);
        var x = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(x, y) * SpawnRadius + (Vector2) transform.position;
    }

    private Vector2 GetAsteroidVelocity(float angle)
    {
        var angleToPlayer = angle + 180;
        var asteroidMovementAngle = Random.Range(angleToPlayer - 45, angleToPlayer + 45);
        var y = Mathf.Cos(asteroidMovementAngle * Mathf.Deg2Rad);
        var x = Mathf.Sin(asteroidMovementAngle * Mathf.Deg2Rad);
        return new Vector2(x, y);
    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.white;
//        float theta = 0;
//        float x = SpawnRadius * Mathf.Cos(theta);
//        float y = SpawnRadius * Mathf.Sin(theta);
//        Vector3 pos = transform.position + new Vector3(x, y);
//        Vector3 newPos = pos;
//        Vector3 lastPos = pos;
//        for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
//        {
//            x = SpawnRadius * Mathf.Cos(theta);
//            y = SpawnRadius * Mathf.Sin(theta);
//            newPos = transform.position + new Vector3(x, y);
//            Gizmos.DrawLine(pos, newPos);
//            pos = newPos;
//        }
//        Gizmos.DrawLine(pos, lastPos);
//    }
}
