using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Asteroid : MonoBehaviour
{
    public int Score = 10;
    
    private GameController gameController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Collider2D col;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        gameController = FindObjectOfType<GameController>();
    }

//    private void OnCollisionEnter2D(Collision2D other)
//    {
//        if (!other.gameObject.CompareTag("Asteroid"))
//        {
//            Destroy(gameObject);
//        }
//    }

    public void OnExplosionEnd()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            col.isTrigger = true;

            spriteRenderer.enabled = false;
            
            animator.SetTrigger("Death");
            
            gameController.UpdateScore(Score);
            AudioSource.PlayClipAtPoint(gameController.SoundsConfigure.AsteroidExplosion, transform.position);
        }
    }
}
