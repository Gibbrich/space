using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class AsteroidSpawner : MonoBehaviour
{
    public GameObject AsteroidPrefab;
    public float AsteroidSpeed = 3;
    public float Frequency = 3;
    public MoveDirection Direction;
    private float lastSpawnTime = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawnTime >= Frequency)
        {
            var spawnPoint = GetSpawnPoint();

            var asteroid = Instantiate(AsteroidPrefab, spawnPoint, Quaternion.identity);
            var asteroidRb = asteroid.GetComponent<Rigidbody>();

            var angle = GetAngle();

            var velocityY = Mathf.Cos(angle * Mathf.Deg2Rad);
            var velocityX = Mathf.Sin(angle* Mathf.Deg2Rad);

            var asteroidRbVelocity = new Vector3(velocityX, velocityY) * AsteroidSpeed;
            asteroidRb.velocity = asteroidRbVelocity;

            lastSpawnTime = Time.time;
        }
    }

    private Vector2 GetSpawnPoint()
    {
        float x;
        float y;
        
        switch (Direction)
        {
            case MoveDirection.UP:
                x = Random.Range(transform.position.x - transform.localScale.x / 2,
                    transform.position.x + transform.localScale.x / 2);
                y = transform.position.y + 2;
                break;
            case MoveDirection.DOWN:
                x = Random.Range(transform.position.x - transform.localScale.x / 2,
                    transform.position.x + transform.localScale.x / 2);
                y = transform.position.y - 2;
                break;
            case MoveDirection.LEFT:
                x = transform.position.x - 2;
                y = Random.Range(transform.position.y - transform.localScale.y / 2,
                    transform.position.y + transform.localScale.y / 2);
                break;
            case MoveDirection.RIGHT:
                x = transform.position.x + 2;
                y = Random.Range(transform.position.y - transform.localScale.y / 2,
                    transform.position.y + transform.localScale.y / 2);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return new Vector2(x, y);
    }

    private float GetAngle()
    {
        switch (Direction)
        {
            case MoveDirection.UP:
                return Random.Range(-90, 90);
            case MoveDirection.DOWN:
                return (Random.Range(-90, 90) + 180);
            case MoveDirection.LEFT:
                return (Random.Range(0, 180) + 180);
            case MoveDirection.RIGHT:
                return Random.Range(0, 180);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}