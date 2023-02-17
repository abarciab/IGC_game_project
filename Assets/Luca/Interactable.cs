using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isContinuousPress;
    //Objects where you must hold down interact to keep interacting with.
    //ex. slowly turning levers that snap back into place when you stop interacting ARE continuous, buttons that you press once to activate are NOT continuous
    
    private GameObject player;
    private IEnumerator coroutine;
    private bool checking = false;
    //player is currently close enough to interact, requiring InteractableCheckCoroutine()
    private bool holdingInteract = false;
    //continuous interactable is currently in a state of being interacted with

    void Start() {
        player = GameObject.Find("player");
        coroutine = InteractableCheckCoroutine();
    }

    void Update() {
        if (Input.GetKey("e")) {
            holdingInteract = true;
        } else {
            holdingInteract = false;
        }
    }
 
    void OnTriggerStay(Collider coll) {
        //all my homies hate ontriggerenter
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
            if (Vector3.Angle(transform.position - player.transform.position, player.transform.forward) <= 60f) {
                //uses smallest vector2 on any plane between player and button
                //print(holdingInteract);
                if (holdingInteract) {
                    gameObject.SendMessage("Activate");
                } else if (isContinuousPress) {
                    gameObject.SendMessage("Deactivate");
                }
            }
            //if (isContinuousPress) { gameObject.SendMessage("Deactivate"); }
            //if the player moves too far away while holding down interact

            yield return new WaitForSeconds(0.1f);
        }
    }
}
