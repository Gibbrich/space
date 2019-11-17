using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private float posX;
    private float posY;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        if (player)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(player);
        }
    }
}
