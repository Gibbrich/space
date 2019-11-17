using System;
using UnityEngine;

public class GunController: MonoBehaviour
{
    public BulletController bulletPrefab;
    private GameController gameController;
    private PlayerController playerController;
    private Transform pivot;
    
    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        playerController = GetComponentInParent<PlayerController>();
        pivot = transform.parent;
    }

    private void Update()
    {
        if (gameController.GameState == GameState.Play)
        {
            var mousePosition = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var pivotPosition = mousePosition - (Vector2) pivot.transform.position;
            var angle = Mathf.Atan2(pivotPosition.y, pivotPosition.x) * Mathf.Rad2Deg;
            pivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            if (Input.GetMouseButtonDown(0))
            {
                var direction = mousePosition - (Vector2) transform.position;

                var bullet = Instantiate(bulletPrefab, (Vector2) transform.position + direction.normalized, Quaternion.identity);
                var bulletRB = bullet.GetComponent<Rigidbody2D>();
                bulletRB.velocity = direction.normalized * bullet.BulletSpeed;

                // todo - если не нормализовать вектор, получается ускорение зависит от расстояния
                // цели. т.е. если игрок захочет получше прицелиться, у него будет увеличенная отдача
                playerController.UpdateVelocity(direction * -1);
            }
        }
    }
}