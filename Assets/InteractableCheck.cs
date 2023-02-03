using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableCheck : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        int layerMask = 1 << 6;

        Debug.DrawRay(transform.position, Camera.main.transform.forward, Color.green, 20f);
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, hitInfo: out hit, 20f, layerMask: layerMask)) {
            Debug.Log(hit.collider.gameObject);
            GameObject.Find("UICanvas").transform.Find("hintText").gameObject.GetComponent<TMP_Text>().text = "Press 'E' to interact with buttons!";
        } else {
            GameObject.Find("UICanvas").transform.Find("hintText").gameObject.GetComponent<TMP_Text>().text = "";
        }
    }
}
