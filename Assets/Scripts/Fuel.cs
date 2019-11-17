
using System;
using UnityEngine;

public class Fuel: MonoBehaviour
{
    public float Value = 30;
    public float Lifetime = 9;
    [Range(0, 1)] public float StartAnimationThreshold;

    private GameController gameController;
    private float createTime;
    private Animator animator;
    private bool isAnimationStarted;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        createTime = Time.time;
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!isAnimationStarted &&Time.time - createTime >= Lifetime * (1 - StartAnimationThreshold))
        {
            animator.SetTrigger("Start");
            isAnimationStarted = true;
        }
        
        if (Time.time - createTime >= Lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().RechargeFuel(Value);
            gameController.UpdateFuelCollectCount();
            AudioSource.PlayClipAtPoint(gameController.SoundsConfigure.BatteryPickUp, transform.position);
            Destroy(gameObject);
        }
    }
}