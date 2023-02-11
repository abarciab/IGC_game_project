using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject connectedObject;
    
    private GameObject player;
    private IEnumerator coroutine;
    private bool checking = false;

    void Start() {
        player = GameObject.Find("player");
        coroutine = InteractableCheckCoroutine();
    }

    void OnTriggerStay(Collider coll) {
        if (!checking && coll.gameObject.tag == "Player") {
            StartCoroutine(coroutine);
            checking = true;
        }
    }

    void OnTriggerExit(Collider coll) {
        if (coll.gameObject.tag == "Player") {
            StopCoroutine(coroutine);
            checking = false;
        }
    }
    
    public IEnumerator InteractableCheckCoroutine() {
        while (true) {
            //Debug.DrawRay(transform.position, transform.forward, Color.blue, 0.1f);
            //Debug.DrawRay(transform.position, interactable.transform.position - transform.position, Color.red, 0.1f);
            if (Vector3.Angle(transform.position - player.transform.position, player.transform.forward) <= 60f) {
                if (Input.GetKey(KeyCode.E)) {
                    gameObject.SendMessage("Activate");
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
