using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Vector3 moveDirection;

    public float speed = 5f;
    public float speedup = 5f;
    public float jumpForce = 10f;
    public float sensitivity = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var forward = Input.GetAxis("Vertical");
        var right = Input.GetAxis("Horizontal");
        var transformComponent = transform;
        var ground = Physics.Raycast(transformComponent.position, Vector3.down, 1.5f);
        var jump = Input.GetButtonDown("Jump") && ground;
        var rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0);

        transformComponent.Rotate(rotation * sensitivity);

        if (jump)
            StartCoroutine(nameof(Jump));

        moveDirection = transformComponent.forward * forward + transformComponent.right * right;
        moveDirection *= speed;

        if (Input.GetKey(KeyCode.LeftShift))
            moveDirection *= speedup;

        rigidBody.velocity = moveDirection + Vector3.up * rigidBody.velocity.y;
    }

    private IEnumerator Jump()
    {
        var currentJumpForce = jumpForce;
        while (currentJumpForce > 0)
        {
            rigidBody.velocity += new Vector3(0, Mathf.Sqrt(currentJumpForce) / 2, 0);
            currentJumpForce -= 1.1f;
            yield return new WaitForFixedUpdate();
        }
    }
}
