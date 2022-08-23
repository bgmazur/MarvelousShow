using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Vector2 movement;


    // Update is called once per frame
    void Update()
    {
       movement.x = Input.GetAxisRaw("Horizontal");
       movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() 
    {
      transform.Translate(movementSpeed * movement.x * Time.deltaTime, movementSpeed * movement.y * Time.deltaTime, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entered collision");
    }
}
