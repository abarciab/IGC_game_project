using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLever : MonoBehaviour
//currently isn't actually rotating, i suspect that i've written the lerp wrong
{
    public float maxRotation;
    public float turnSpeed = 5f;
    public GameObject connectedObject;

    private IEnumerator coroutine;
    private Quaternion initRot;
    private Quaternion targetRot;

    void Start() {
        initRot = transform.localRotation;
        targetRot = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z - maxRotation);
    }

    public void Activate() {
        //print("activate");
        if (coroutine != RotateForward()) {
            if (coroutine != null) {StopCoroutine(coroutine);}
            coroutine = RotateForward();
            StartCoroutine(coroutine);
        }
    }

    public void Deactivate() {
        //print("deactivate");
        if (coroutine != RotateBackward()) {
            if (coroutine != null) {StopCoroutine(coroutine);}
            coroutine = RotateBackward();
            StartCoroutine(coroutine);
        }
    }

    IEnumerator RotateForward() {
        connectedObject.SendMessage("Activate");
        while(true) {
            float newAngle = Mathf.Lerp(transform.localRotation.z, transform.localRotation.z - 10f, Time.deltaTime * turnSpeed);
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, newAngle);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator RotateBackward() {
        connectedObject.SendMessage("Deactivate");
        while(true) {
            float newAngle = Mathf.Lerp(transform.localRotation.z, transform.localRotation.z + 10f, Time.deltaTime * turnSpeed);
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, newAngle);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
