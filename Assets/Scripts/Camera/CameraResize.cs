using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResize : MonoBehaviour
{
    private float size;
    private float resize;
    private float speed;

    public Rigidbody rb;
    public float maxResize = 10f;

    void Start()
    {
        speed = 1f;
        size = Camera.main.orthographicSize;
    }

    void Update()
    {
        Debug.Log(rb.velocity);

        resize = size + 1f * Mathf.Abs(rb.velocity.x);
        if (resize >= maxResize) resize = 8f;
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, resize, speed * Time.deltaTime);
    }
}
