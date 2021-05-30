using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public float speed = 5;

    float screenHalfWidthInWorldUnits;
    float screenVerticalLimitInWorldUnits;

    public event Action OnPlayerDead;
    // Start is called before the first frame update
    void Start()
    {
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + transform.localScale.x /2;
        screenVerticalLimitInWorldUnits = Camera.main.orthographicSize * 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        ReceivedInput();
    }

    void ReceivedInput()
    {
        Vector3 input = new Vector3(Input.acceleration.x, 0, 0);
        Vector3 velocity = input.normalized * speed;

        transform.Translate(velocity * Time.deltaTime);

        float xPosition = transform.position.x;
        float yPosition = transform.position.y;
        if (xPosition < -screenHalfWidthInWorldUnits)
        {
            xPosition = screenHalfWidthInWorldUnits;
        }
        else if (xPosition > screenHalfWidthInWorldUnits)
        {
            xPosition = -screenHalfWidthInWorldUnits;
        }
        if (yPosition > screenVerticalLimitInWorldUnits)
        {
            yPosition = screenVerticalLimitInWorldUnits;
        }
        else if (yPosition < -screenVerticalLimitInWorldUnits)
        {
            yPosition = -screenVerticalLimitInWorldUnits;
        }
        transform.position = new Vector3(xPosition, yPosition, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Danger")
        {
            if(OnPlayerDead != null)
            {
                OnPlayerDead();
            }
            Destroy(this.gameObject);
        }
    }
}
