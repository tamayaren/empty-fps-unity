using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public bool CanMove = true;

    private Rigidbody rigidbody;

    [SerializeField]
    private float speed = 50f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!CanMove) return;

        Vector2 input = Vector2.zero;

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        rigidbody.AddRelativeForce(new Vector3(Mathf.Clamp(input.x * speed, -speed/2, speed/2), 0, input.y) * Time.fixedDeltaTime);
    }
}
