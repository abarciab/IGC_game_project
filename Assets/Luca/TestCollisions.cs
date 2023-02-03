using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollisions : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.transform.gameObject.name);
    }
}
