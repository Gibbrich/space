using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider2D))] 
public class AsteroidSpawnerSquare: MonoBehaviour
{
    public AsteroidSpawnerConfig Config;
    public float AdditionalHeight = 1;
    
    private GameController gameController;
    private BoxCollider2D spawnCollider;
    private float lastSpawnTime = 0;
    
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        spawnCollider = GetComponent<BoxCollider2D>();
        spawnCollider.size = GetSpawnerSize();
    }
    
    void Update()
    {
        if (Time.timeSinceLevelLoad - lastSpawnTime >= Config.Frequency && gameController.GameState != GameState.Stop)
        {
            for (var i = 0; i < Config.SpawnAtATime; i++)
            {
                Spawn();
            }
            lastSpawnTime = Time.timeSinceLevelLoad;
        }
    }

    private void Spawn()
    {
        var spawnPosition = GetSpawnPosition();
        var asteroidRotation = Random.Range(0, 360);
        var id = Random.Range(0, Config.AsteroidPrefabs.Count - 1);
        var asteroid = Instantiate(Config.AsteroidPrefabs[id], spawnPosition, Quaternion.AngleAxis(asteroidRotation, Vector3.forward));
        var size = Random.Range(Config.AsteroidSizeMin, Config.AsteroidSizeMax);
        asteroid.transform.localScale = Vector3.one * size;
        var asteroidRb = asteroid.GetComponent<Rigidbody2D>();

        var asteroidSpeed = Random.Range(Config.AsteroidSpeedMin, Config.AsteroidSpeedMax);
        var velocity = GetAsteroidVelocity(spawnPosition) * asteroidSpeed;
        asteroidRb.velocity = velocity;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);

        if (other.gameObject.CompareTag("Player"))
        {
            gameController.EndGame();
        }
    }

    private Vector2 GetSpawnerSize()
    {
        var height = (Camera.main.orthographicSize + AdditionalHeight) * 2;
        var width = Camera.main.aspect * height;
        return new Vector2(width, height);
    }

    private Vector2 GetSpawnPosition()
    {
        var size = GetSpawnerSize();
        var halfWidth = size.x / 2;
        var halfHeight = size.y / 2;
        var radius = Mathf.Sqrt(Mathf.Pow(halfHeight, 2) + Mathf.Pow(halfWidth, 2));

        var angle = Random.Range(0, 360) * Mathf.Deg2Rad;

        var x = Mathf.Clamp(Mathf.Sin(angle) * radius, -halfWidth, halfWidth);
        var y = Mathf.Clamp(Mathf.Cos(angle) * radius, -halfHeight, halfHeight);
        return new Vector2(x, y);
    }

    private Vector2 GetAsteroidVelocity(Vector2 spawnPosition)
    {
        var direction = (Vector2) transform.position - spawnPosition;
        var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        var asteroidMovementAngle = Random.Range(angle - 45, angle + 45);
        var y = Mathf.Cos(asteroidMovementAngle * Mathf.Deg2Rad);
        var x = Mathf.Sin(asteroidMovementAngle * Mathf.Deg2Rad);
        return new Vector2(x, y);
    }

    private void OnDrawGizmos()
    {
        // todo - implement debug drawing
    }
}