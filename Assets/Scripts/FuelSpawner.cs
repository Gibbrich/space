
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FuelSpawner: MonoBehaviour
{
    public float SpawnRadiusMin = 10;
    public float SpawnRadiusMax = 15;
    public Fuel FuelPrefab;
    public float SpawnFrequency = 1.5f;
    
    private float lastSpawnTime = 0;
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        if (Time.time - lastSpawnTime >= SpawnFrequency)
        {
            Spawn();
            lastSpawnTime = Time.time;
        }
    }

    private void Spawn()
    {
        var angle = Random.Range(0, 360);
        var spawnPosition = GetSpawnPosition(angle);
        var fuel = Instantiate(FuelPrefab, spawnPosition, Quaternion.identity);
        gameController.Fuel = fuel;
    }
    
    private Vector2 GetSpawnPosition(float angle)
    {
        var y = Mathf.Cos(angle * Mathf.Deg2Rad);
        var x = Mathf.Sin(angle * Mathf.Deg2Rad);
        var distance = Random.Range(SpawnRadiusMin, SpawnRadiusMax);
        return new Vector2(x, y) * distance + (Vector2) transform.position;
    }
}