using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    Rigidbody rb;
    float initZScale;
    bool activated = false;

    [SerializeField]
    List<GameObject> connectedObject = new List<GameObject>();

    void Start() {
        rb = GetComponent<Rigidbody>();
        initZScale = transform.localScale.z;
    }

    public IEnumerator Activate() {
        float dilation = 1;
        Vector3 newDilation = transform.localScale;
        while (dilation > 0.3f) {
            // contracts to 30% of original size
            newDilation.z = initZScale * dilation;
            transform.localScale = newDilation;
            dilation -= 1.4f * Time.deltaTime;
            //takes (0.7 / 1.4 = 0.5 seconds)    
            yield return new WaitForSeconds(0.01f);        
        }
        newDilation.z = initZScale * 0.3f;
        transform.localScale = newDilation;

        if (!activated) {
            SendActivate();
        } else {
            SendDeactivate();
        }
        activated = !activated;

        while (dilation < 1) {
            // returns to 100% of original size
            newDilation.z = initZScale * dilation;
            transform.localScale = newDilation;
            dilation += 1.4f * Time.deltaTime;
            //takes (0.7 / 1.4 = 0.5 seconds)
            yield return new WaitForSeconds(0.01f);             
        }
        newDilation.z = initZScale;
        transform.localScale = newDilation;
        yield return null;
    }

    void SendActivate() {
        foreach(var c in connectedObject) c.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
    }

    void SendDeactivate() {
        //Debug.Log("button has been deactivated");
        foreach (var c in connectedObject) c.SendMessage("Deactivate", SendMessageOptions.DontRequireReceiver);
    }

}