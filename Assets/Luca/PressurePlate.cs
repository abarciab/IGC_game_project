using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    float springForce = 0.2f;
    //how strongly the pressure plate resists being pushed down
    Rigidbody rb;
    float initY;
    bool resettingPlate = false;
    bool activated = false;

    [SerializeField]
    GameObject poweredObject;
    //gameobject that gets activated when this pressure plate is activated, and deactivated when it is deactivated

    void Start() {
        rb = GetComponent<Rigidbody>();
        initY = transform.localPosition.y;
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.transform == transform.parent.Find("PressurePlateFloor")){ SendActivate(); }
        if(!resettingPlate){ StartCoroutine(ResetPlate()); }
    }

    void OnCollisionExit(Collision collision) {
        if(activated && collision.transform == transform.parent.Find("PressurePlateFloor")){ SendDeactivate(); }
    }

    public IEnumerator ResetPlate() {
        //resets pressure plate
        while (Mathf.Abs(transform.localPosition.y - initY) > 0.1f) {
            rb.AddForce(transform.up * springForce);
            yield return new WaitForSeconds(0.01f);
        }
        rb.velocity = new Vector3(0, 0, 0);
        transform.localPosition = new Vector3(transform.localPosition.x, initY, transform.localPosition.z);
        resettingPlate = false;
        yield return null;
    }

    void SendActivate() {
        activated = true;
        poweredObject.SendMessage("Activate");
    }

    void SendDeactivate() {
        activated = false;
        poweredObject.SendMessage("Deactivate");
    }
}
