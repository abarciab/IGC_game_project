using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 mouseLookSpeed;
    [SerializeField] float friction = 0.05f;
    [SerializeField] float turnSpeed = 0.1f;
    [SerializeField] float surfaceOffset = 1.5f;
    Rigidbody rb;
    bool mouseLocked;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mouseLocked = true;
    }

    private void Update()
    {
        MouseLook();
        Move();

        if (Input.GetKeyDown(KeyCode.Escape)) mouseLocked = !mouseLocked;
        LockMouse();
    }

    void Move()
    {
        
        
        Vector3 dir = GetInputDir();
        if (dir == Vector3.zero) {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, friction);
        }

        else if (Vector3.Project(rb.velocity, dir).magnitude < (speed * 1000)) {
            rb.velocity = Vector3.Lerp(rb.velocity, dir * (speed * 1000) * Time.deltaTime, turnSpeed);
        }

        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, 0.05f);
        LerpToZZeroRot();

        if (!GameManager.instance.oceanSurface) return;
        if (transform.position.y > GameManager.instance.oceanSurface.transform.position.y + surfaceOffset) transform.position = new Vector3(transform.position.x, GameManager.instance.oceanSurface.transform.position.y + surfaceOffset, transform.position.z);
    }

    void LerpToZZeroRot()
    {
        var desired = transform.localEulerAngles;
        desired.z %= 360;
        if (desired.z > 180) desired.z = 360;
        else if (desired.z < -180) desired.z = -360;
        else desired.z = 0;
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, desired, friction);
    }

    Vector3 GetInputDir()
    {
        if (Input.GetKey(KeyCode.W)) {
            return transform.forward;
        }
        if (Input.GetKey(KeyCode.S)) {
            return -1 * transform.forward;
        }
        return Vector3.zero;
    }

    void LockMouse()
    {
        Cursor.lockState = mouseLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !mouseLocked;
    }

    void MouseLook()
    {
        var mouseX = Input.GetAxis("Mouse X") * mouseLookSpeed.x;
        var mouseY = Input.GetAxis("Mouse Y") * -mouseLookSpeed.y;

        transform.localEulerAngles += new Vector3(mouseY * Time.deltaTime, mouseX * Time.deltaTime, 0);
    }
}
