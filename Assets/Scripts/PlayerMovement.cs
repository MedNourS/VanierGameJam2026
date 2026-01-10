
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float smoothness;
    private Vector3 newPosition = Vector3.zero;
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;
    [SerializeField] private Rigidbody2D rb;
    private void Update()
    {
        HandleMovement();
    }


    private void HandleMovement()
    {
        if(Input.GetKey(KeyCode.RightArrow))
            rb.
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.DownArrow))
            transform.Translate(-Vector3.up * moveSpeed * Time.deltaTime);

    }
}
