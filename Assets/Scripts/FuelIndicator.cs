using System;
using UnityEngine;

public class FuelIndicator: MonoBehaviour
{
    public float Radius = 100;
    [Tooltip("Used for correct rotation of arrow sprite, so it will point in correct location")]
    public float ArrowRotationAngle = 180;
    
    private GameController gameController;
    private RectTransform canvas;
    private PlayerController playerController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        playerController = FindObjectOfType<PlayerController>();
        canvas = FindObjectOfType<UIController>().GetComponent<RectTransform>();
    }

    private void Update()
    {
        var fuel = gameController.Fuel;
        if (fuel)
        {
            var offset = fuel.transform.position - playerController.transform.position;
            var angle = Mathf.Atan2(offset.x, offset.y);

            var y = Mathf.Cos(angle);
            var x = Mathf.Sin(angle);

            transform.position = new Vector2(x, y) * Radius + (Vector2) canvas.transform.position;

            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg + ArrowRotationAngle, Vector3.back);
        }
    }
}