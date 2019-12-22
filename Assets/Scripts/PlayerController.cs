using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    public BulletController bulletPrefab;
//    public float MaxFuelValue = 100f;
//    public float FuelConsumption = 1;
    
    private Rigidbody2D rb;
    private GameController gameController;
    private AudioSource audioSource;

//    private float currentFuelValue;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
//        currentFuelValue = MaxFuelValue;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = gameController.SoundsConfigure.Ship;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
//        if (gameController.GameState == GameState.Play)
//        {
//            currentFuelValue -= Time.deltaTime * FuelConsumption;
//            NotifyGameControllerFuelChanged();
//        }
    }

    public void RechargeFuel(float amount)
    {
//        currentFuelValue = Mathf.Clamp(currentFuelValue + amount, 0, MaxFuelValue);
//        NotifyGameControllerFuelChanged();
    }

    public void UpdateVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    public Vector2 GetVelocity() => rb.velocity;

//    private void NotifyGameControllerFuelChanged() => gameController.UpdateFuel(currentFuelValue / MaxFuelValue);

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);

        gameController.PlayAudio(gameController.SoundsConfigure.ShipExplosion);
        gameController.EndGame();
    }
}
