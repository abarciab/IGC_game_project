using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SlidingDoor : MonoBehaviour
{

    [SerializeField] bool setOpen, setClosed, startOpen;
    [SerializeField] Vector3 openPos, closePos;
    bool opening, closing;
    bool open;

    private void Update()
    {

        if (setOpen) {
            setOpen = false;
            openPos = transform.position;
        }

        if (setClosed) {
            setClosed = false;
            closePos = transform.position;
        }

        if (!Application.isPlaying) return;

        var targetPos = transform.position;
        if (opening) targetPos = openPos;
        else if (closing) targetPos = closePos;

        if (targetPos == transform.position || (targetPos != openPos && targetPos != closePos) ) return;
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.025f);
        if (Vector3.Distance(transform.position, targetPos) <= 0.01f) {
            opening = closing = false;
            open = targetPos == openPos;
        }
    }

    private void Start()
    {
        if (!startOpen) Close();
        else Open();
    }

    void Open()
    {
        opening = true;
        closing = false;
    }

    void Close()
    {
        closing = true;
        opening = false;
    }

    void Toggle()
    {
        if (open) Close();
        else Open();
    }

    void Activate() {
        Toggle();
    }
}
