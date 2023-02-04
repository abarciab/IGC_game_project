using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceExplosionTrigger : MonoBehaviour
{
    public GameObject wreckingBall;
    bool triggered = false;

    void OnTriggerEnter(Collider collider) {
        if (!triggered){
            if (collider.gameObject == wreckingBall) {
                triggered = true;
                Debug.Log("wrecking ball hit");
                foreach (Collider coll in Physics.OverlapSphere(transform.position, 50f)) {
                    if (coll.gameObject.name.Substring(0, 5) == "crate") {
                        Vector3 force = 100000f * (coll.transform.position - transform.position);
                        Debug.Log(force);
                        coll.gameObject.GetComponent<Rigidbody>().AddForce(force);
                    }
                }
                 Destroy(gameObject);
            }
        }
    }
}
