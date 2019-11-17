
using System;
using UnityEngine;

public class Fuel: MonoBehaviour
{
    public float Value = 30;
    public float Lifetime = 9;

    private float createTime;

    private void Start()
    {
        createTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - createTime >= Lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().RechargeFuel(Value);
            Destroy(gameObject);
        }
    }
}