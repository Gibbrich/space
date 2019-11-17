
using System;
using UnityEngine;

public class Fuel: MonoBehaviour
{
    public float Value = 30;
    public float Lifetime = 9;

    private GameController gameController;
    private float createTime;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        createTime = Time.time;
    }

    private void Update()
    {
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
            AudioSource.PlayClipAtPoint(gameController.SoundsConfigure.BatteryPickUp, transform.position);
            Destroy(gameObject);
        }
    }
}