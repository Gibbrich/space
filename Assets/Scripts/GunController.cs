using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunController : MonoBehaviour
{
    public BulletController bulletPrefab;
    public Transform Barrel;

    [Tooltip("Used for correct rotation of gun sprite, so it will point in correct location")]
    public float GunSpriteRotationAngle = 133;
    public float MaxRecoil = 5;
    public float BulletSpriteRotationAngle = 0;
    private GameController gameController;
    private UIController uiController;
    private PlayerController playerController;
    private Transform pivot;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        playerController = GetComponentInParent<PlayerController>();
        uiController = FindObjectOfType<UIController>();
        pivot = transform.parent;
    }

    private void Update()
    {
        if (gameController.GameState == GameState.Play)
        {
#if UNITY_ANDROID || UNITY_IOS
            var joysticDirection = uiController.GetJoysticDirection();
            var angle = Mathf.Atan2(joysticDirection.y, joysticDirection.x) * Mathf.Rad2Deg;
#else
            var mousePosition = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var pivotPosition = mousePosition - (Vector2) pivot.transform.position;
            var angle = Mathf.Atan2(pivotPosition.y, pivotPosition.x) * Mathf.Rad2Deg;
#endif
            pivot.transform.rotation = Quaternion.AngleAxis(angle + GunSpriteRotationAngle, Vector3.forward);

#if UNITY_ANDROID || UNITY_IOS
            // used for convenient debugging in Editor 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
#else
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(GetPointerId()))
            {  
              Fire();
            }
#endif
        }
    }

    public void Fire()
    {
        var transformPosition = transform.position;

#if UNITY_ANDROID || UNITY_IOS
        var joysticDirection = uiController.GetJoysticDirection();
        var angle = Mathf.Atan2(joysticDirection.y, joysticDirection.x) * Mathf.Rad2Deg;

        var bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        var bullet = Instantiate(bulletPrefab, (Vector2) Barrel.position, bulletRotation);
        var bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = joysticDirection.normalized * bullet.BulletSpeed;

        // todo - если не нормализовать вектор, получается ускорение зависит от расстояния
        // цели. т.е. если игрок захочет получше прицелиться, у него будет увеличенная отдача

        var velocity = playerController.GetVelocity() + joysticDirection.normalized * -1;
        if (velocity.magnitude >= MaxRecoil)
        {
            velocity = velocity.normalized * MaxRecoil;
        }

        playerController.UpdateVelocity(velocity);
#else
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
#endif


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