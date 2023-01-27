using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject camParent;
    [SerializeField] PostProcessVolume underwaterPP;

    private void Update()
    {
        camParent.transform.position = player.transform.position;


        camParent.transform.rotation = player.transform.rotation;
        camParent.transform.localEulerAngles = new Vector3(camParent.transform.localEulerAngles.x, camParent.transform.localEulerAngles.y, 0);

        if (GameManager.instance != null) underwaterPP.enabled = transform.position.y < GameManager.instance.oceanSurface.transform.position.y; 
    }

}
