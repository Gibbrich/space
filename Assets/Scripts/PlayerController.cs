using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    public BulletController bulletPrefab;
    public float MaxFuelValue = 100f;
    public float FuelConsumption = 1;
    
    private Rigidbody2D rb;
    private GameController gameController;
    private float currentFuelValue;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
        currentFuelValue = MaxFuelValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GameState == GameState.Play)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var target = (Vector2) Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

                var direction = target - (Vector2) transform.position;

                var bullet = Instantiate(bulletPrefab, (Vector2) transform.position + direction.normalized, Quaternion.identity);
                var bulletRB = bullet.GetComponent<Rigidbody2D>();
                bulletRB.velocity = direction.normalized * bullet.BulletSpeed;

                // todo - если не нормализовать вектор, получается ускорение зависит от расстояния
                // цели. т.е. если игрок захочет получше прицелиться, у него будет увеличенная отдача
                rb.velocity = direction * -1;
            }
        }

        currentFuelValue -= Time.deltaTime * FuelConsumption;
        NotifyGameControllerFuelChanged();
    }

    public void RechargeFuel(float amount)
    {
        currentFuelValue = Mathf.Clamp(currentFuelValue + amount, 0, MaxFuelValue);
        NotifyGameControllerFuelChanged();
    }

    private void NotifyGameControllerFuelChanged() => gameController.UpdateFuel(currentFuelValue / MaxFuelValue);

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        gameController.EndGame();
    }
}
