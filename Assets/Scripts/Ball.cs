using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Vector3 forceDirection;

    [SerializeField]
    private float kickPower = 1;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, forceDirection, Color.red);
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = new Vector3(0, 1, -1.2f);
            rigidbody.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        var position = GetComponent<Transform>().position;
        var rawDirection = position - other.transform.position;
        var force = new Vector3(rawDirection.x, 0, rawDirection.y) * (kickPower * rigidbody.mass);
        forceDirection = force;
        rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
