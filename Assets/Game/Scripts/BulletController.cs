using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    public float BulletSpeed = 5;
    public float Lifetime = 5;

    private float creationTime;

    private void Start()
    {
        creationTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - creationTime >= Lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }
}
