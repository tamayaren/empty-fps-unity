using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public bool CanRotate = true;

    private GameObject player;
    private Camera camera;

    [SerializeField] private Vector3 offset = new Vector3(0, 1f, 0);
    [SerializeField] [Range(.5f, 2f)] private float sensitivity = 1f;
    [SerializeField] [Range(0f, 90f)] private float rotationLimit = 88f;
    [SerializeField] [ReadOnly] private Vector2 rotation = Vector2.zero;
    [SerializeField] private float speed = 500f;

    protected const string xAxis = "Mouse X";
    protected const string yAxis = "Mouse Y";

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectWithTag("Player");
        camera = Camera.main;
    }

    private void Update()
    {
        if (!this.CanRotate) return;
        Transform transform = player.transform;

        this.camera.transform.position = transform.position + offset;
        //

        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -this.rotationLimit, this.rotationLimit);

        var xQuaternion = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuaternion = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQuaternion * yQuaternion;
        this.camera.transform.rotation = Quaternion.RotateTowards(this.camera.transform.rotation, transform.localRotation, (this.speed) * Time.deltaTime);
    }
}
