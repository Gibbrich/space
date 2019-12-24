using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    public BulletController bulletPrefab;
    public SpriteRenderer ShipRenderer;
    public SpriteRenderer GunRenderer;
    public SpriteRenderer ExplosionRenderer;

    public State State = State.ALIVE; 
    private Rigidbody2D rb;
    private CircleCollider2D col;
    private GameController gameController;
    private AudioSource audioSource;
    private Animator animator;

//    private float currentFuelValue;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        gameController = FindObjectOfType<GameController>();
//        currentFuelValue = MaxFuelValue;
        animator = GetComponent<Animator>();
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

    public void OnExplosionEnd()
    {
        // todo - without disabling ExplosionRenderer there will remain 
        // smoke sprite until game over. Better to fix sprite sheet
        ExplosionRenderer.enabled = false;
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        gameController.EndGame();
        Destroy(gameObject);
    }

//    private void NotifyGameControllerFuelChanged() => gameController.UpdateFuel(currentFuelValue / MaxFuelValue);

    private void OnCollisionEnter2D(Collision2D other)
    {
        // turn off collision with asteroids and set speed to 0
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        col.isTrigger = true;

        ShipRenderer.enabled = false;
        GunRenderer.enabled = false;

        State = State.DEAD;

        animator.SetTrigger("Death");
        gameController.PlayAudio(gameController.SoundsConfigure.ShipExplosion);
    }
}
