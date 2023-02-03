using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{

    Rigidbody rb;
    float doorForce = 10f;
    //force with which door closes
    float initY;
    float slideDistance;
    Coroutine doorMovement;

    void Start() {
        rb = GetComponent<Rigidbody>();
        initY = transform.localPosition.y;
        slideDistance = GetComponent<MeshRenderer>().bounds.size.y;
    }

    public IEnumerator CloseDoor() {
        while (Mathf.Abs(transform.localPosition.y - initY) > 0.1f) {
            rb.AddForce(-transform.up * doorForce);
            yield return new WaitForSeconds(0.01f);
        }
        rb.velocity = new Vector3(0, 0, 0);
        transform.localPosition = new Vector3(transform.localPosition.x, initY, transform.localPosition.z);
        yield return null;
    }

    public IEnumerator OpenDoor() {
        while (Mathf.Abs(transform.localPosition.y - initY - slideDistance) > 0.1f) {
            rb.AddForce(transform.up * doorForce);
            yield return new WaitForSeconds(0.01f);
        }
        rb.velocity = new Vector3(0, 0, 0);
        transform.localPosition = new Vector3(transform.localPosition.x, initY + slideDistance, transform.localPosition.z);
        yield return null;
    }

    void Activate() {
        if (doorMovement != null) { StopCoroutine(doorMovement); }
        doorMovement = StartCoroutine(OpenDoor());
    }

    void Deactivate() {
        if (doorMovement != null) { StopCoroutine(doorMovement); }
        doorMovement = StartCoroutine(CloseDoor());
    }
}
