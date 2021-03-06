using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunController : MonoBehaviour
{
    public BulletController bulletPrefab;

    [Tooltip("Used for correct rotation of gun sprite, so it will point in correct location")]
    public float GunSpriteRotationAngle = 133;
    public float BulletSpriteRotationAngle = 0;
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
            pivot.transform.rotation = Quaternion.AngleAxis(angle + GunSpriteRotationAngle, Vector3.forward);

            if (Input.GetMouseButtonDown(0) && playerController.State == State.ALIVE && !EventSystem.current.IsPointerOverGameObject(GetPointerId()))
            {  
                Fire();
            }
        }
    }

    public void Fire()
    {
        var transformPosition = transform.position;

        var mousePosition = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var pivotPosition = mousePosition - (Vector2) pivot.transform.position;
        var angle = Mathf.Atan2(pivotPosition.y, pivotPosition.x) * Mathf.Rad2Deg;
        var direction = mousePosition - (Vector2) transformPosition;

        var bulletRotation = Quaternion.AngleAxis(angle + BulletSpriteRotationAngle, Vector3.forward);
        var bullet = Instantiate(bulletPrefab, (Vector2) transformPosition + direction.normalized,
            bulletRotation);
        var bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = direction.normalized * bullet.BulletSpeed;

        // todo - если не нормализовать вектор, получается ускорение зависит от расстояния
        // цели. т.е. если игрок захочет получше прицелиться, у него будет увеличенная отдача
        playerController.UpdateVelocity(direction * -1);


        AudioSource.PlayClipAtPoint(gameController.SoundsConfigure.Shot, transformPosition);
    }

    private int GetPointerId()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                return Input.GetTouch(0).fingerId;
            }
            else
            {
                return PointerInputModule.kMouseLeftId;
            }
        }
        else
        {
            return PointerInputModule.kMouseLeftId;
        }
    }
}