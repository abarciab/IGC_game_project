using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    Rigidbody rb;
    float initZScale;

    [SerializeField]
    GameObject poweredObject;
    //gameobject that gets activated when this pressure plate is activated, and deactivated when it is deactivated

    void Start() {
        rb = GetComponent<Rigidbody>();
        initZScale = transform.localScale.z;
    }

    public IEnumerator Pressed() {
        float dilation = 1;
        Vector3 newDilation = transform.localScale;
        while (dilation > 0.3f) {
            // contracts to 30% of original size
            newDilation.z = initZScale * dilation;
            transform.localScale = newDilation;
            dilation -= 0.35f * Time.deltaTime;
            //takes (0.7 / .35 = 2 seconds)    
            yield return new WaitForSeconds(0.01f);        
        }
        newDilation.z = initZScale * 0.3f;
        transform.localScale = newDilation;
        SendActivate();
        while (dilation < 1) {
            // returns to 100% of original size
            newDilation.z = initZScale * dilation;
            transform.localScale = newDilation;
            dilation += 0.7f * Time.deltaTime;
            //takes (0.7 / 1 = 1 seconds)
            yield return new WaitForSeconds(0.01f);             
        }
        newDilation.z = initZScale;
        transform.localScale = newDilation;
        yield return null;
    }

    void SendActivate() {
        Debug.Log("button activated");
        poweredObject.SendMessage("Activate");
    }

    void SendDeactivate() {
        Debug.Log("button has been deactivated");
        poweredObject.SendMessage("Deactivate");
    }

}