using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[ExecuteAlways]
public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject camTarget;
    [SerializeField] GameObject camParent;
    [SerializeField] PostProcessVolume underwaterPP;
    [SerializeField] float rotSmoothness = 0.025f;
    [SerializeField] float posSmoothness = 0.025f;

    [SerializeField] Vector2 input;

    private void FixedUpdate()
    {
        camParent.transform.position =  Vector3.Lerp(camParent.transform.position, player.transform.position, posSmoothness);
        transform.LookAt(camTarget.transform);

        float horizontal = input.x;
        float vertical = input.y;
        horizontal = player.transform.eulerAngles.y;
        vertical = player.transform.eulerAngles.x;

        camParent.transform.rotation = Quaternion.Lerp(camParent.transform.rotation, Quaternion.Euler(vertical, horizontal, 0), rotSmoothness);

        if (!Application.isPlaying) return;
        if (GameManager.instance.oceanSurface) underwaterPP.enabled = transform.position.y < GameManager.instance.oceanSurface.transform.position.y; 
    }

}
