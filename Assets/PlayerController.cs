using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class PlayerController : MonoBehaviour
{
    public BulletController bulletPrefab;
    private Rigidbody rb;
    private GameController gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameController.GameState == GameState.Play)
        {
            var target = (Vector2) Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

            var direction = target - (Vector2) transform.position;

            var bullet = Instantiate(bulletPrefab, (Vector2) transform.position + direction.normalized, Quaternion.identity);
            var bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.velocity = direction.normalized * bullet.BulletSpeed;

            // todo - если не нормализовать вектор, получается ускорение зависит от расстояния
            // цели. т.е. если игрок захочет получше прицелиться, у него будет увеличенная отдача
            rb.velocity = direction * -1;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
        gameController.EndGame();
    }
}
