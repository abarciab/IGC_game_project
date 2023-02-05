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
        LerpToZZeroRot();
        
        Vector3 dir = GetInputDir();
        if (dir == Vector3.zero) {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, friction);
        }

        if (Vector3.Project(rb.velocity, dir).magnitude < speed) {
            rb.velocity = Vector3.Lerp(rb.velocity, dir * speed * Time.deltaTime, turnSpeed);
        }

        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, 0.05f);
        if (!GameManager.instance.oceanSurface) return;
        if (transform.position.y > GameManager.instance.oceanSurface.transform.position.y + surfaceOffset) transform.position = new Vector3(transform.position.x, GameManager.instance.oceanSurface.transform.position.y + surfaceOffset, transform.position.z);
    }

    void LerpToZZeroRot()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
    }

    Vector3 GetInputDir()
    {
        if (Input.GetKey(KeyCode.W)) {
            return transform.forward;
        }
        if (Input.GetKey(KeyCode.S)) {
            return -1 * transform.forward;
        }
        if (Input.GetKey(KeyCode.A)) {
            return transform.right * -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            return transform.right;
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
