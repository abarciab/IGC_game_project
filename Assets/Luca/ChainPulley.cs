using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainPulley : MonoBehaviour
{

    public GameObject pulleyTop, pulleyBottom, primaryChainsWrapper, pulleyLinkPrefab;

    [SerializeField] private float chainUpSpeed = 0.05f, chainFallSpeed = 0.15f;    
    [SerializeField]
    private float chainLength = 2f;
    [SerializeField] float chainCount = 0;
    [SerializeField] bool hasHook;

    private List<GameObject> chainList = new List<GameObject>();
    int nameCounter = 0;
    IEnumerator coroutine;
    bool movingUp;

    public GameObject hookedObj;
    [SerializeField] Vector3 hookOffset;

    void Start()
    {
        InitChains();
        Deactivate();
    }

    void InitChains() {
        Vector3 spawnLocation = new Vector3(0, pulleyTop.transform.localPosition.y - 0.1f, 0);
        while(spawnLocation.y - 2.5f > pulleyBottom.transform.localPosition.y + 0.1f) {
            GameObject newChain = Instantiate(pulleyLinkPrefab, primaryChainsWrapper.transform);
            newChain.name = "chain" + nameCounter;
            nameCounter++;
            newChain.transform.localPosition = spawnLocation;
            spawnLocation.y -= 2.5f;
            if (hasHook) break;
        }
        foreach (Transform child in primaryChainsWrapper.transform) {
            chainList.Add(child.gameObject);
        }
        chainCount = hasHook ? 0 :chainLength;

    }

    private void Update()
    {
        if (movingUp) MoveUp();
        else MoveDown();
        ClipChains();
        if (hasHook && hookedObj != null) hookedObj.transform.position = GetBottomChain().transform.position;
    }

    void MoveUp()
    {
        if (!CanMoveUp()) return;
        primaryChainsWrapper.transform.localPosition += Vector3.up * chainUpSpeed;
        if (hasHook || GetBottomChain().transform.position.y < pulleyBottom.transform.position.y) return;

        AddChain(true);
    }

    bool CanMoveUp()
    {
        if (!hasHook) return chainCount > 0;
        return pulleyTop.transform.position.y - GetBottomChain().transform.position.y > 2.5f;
    }

    void MoveDown()
    {
        if (!CanMoveDown()) return;
        primaryChainsWrapper.transform.localPosition += Vector3.down * chainUpSpeed;
        if (GetTopChain().transform.position.y > pulleyTop.transform.position.y) return;

        AddChain(false);
    }

    bool CanMoveDown()
    {
        if (hasHook) return chainCount < chainLength;
        return chainCount < chainLength && GetTopChain().transform.position.y >= pulleyTop.transform.position.y;
    }

    void ClipChains()
    {
        bool clipTop = true, clipBottom = true;
        if (hasHook) clipBottom = false;

        List<GameObject> toDestroy = new List<GameObject>();
        foreach (var c in chainList) if (ShouldClipChain(c, clipTop, clipBottom)) toDestroy.Add(c);
        for (int i = 0; i < toDestroy.Count; i++) {
            KillChain(toDestroy[i]);
        }
    }

    bool ShouldClipChain(GameObject chain, bool clipT, bool clipB)
    {
        float offset = 2.5f;
        if (clipT && chain.transform.position.y >= pulleyTop.transform.position.y + offset) {chainCount--; return true; }
        if (clipB && chain.transform.position.y <= pulleyBottom.transform.position.y - offset) return true;

        return false;
    }

    void KillChain(GameObject chain)
    {
        chainList.Remove(chain);
        Destroy(chain);
    }

    void AddChain(bool addOnBotton)
    {
        GameObject chain = addOnBotton ? GetBottomChain() : GetTopChain();
        float offset = addOnBotton ? -2.5f : 2.5f;
        chainCount += addOnBotton ? 0 : 1;

        GameObject newChain = Instantiate(pulleyLinkPrefab, primaryChainsWrapper.transform);
        newChain.transform.localPosition = new Vector3(0, chain.transform.localPosition.y + offset, 0);
        chainList.Add(newChain);

        newChain.name = "chain" + nameCounter;
        nameCounter++;
    }

    GameObject GetTopChain()
    {
        if (chainList.Count == 0) return null;
        GameObject topChain = chainList[0];
        foreach (var c in chainList) if (c.transform.position.y > topChain.transform.position.y) topChain = c;
        return topChain;
    }
    GameObject GetBottomChain()
    {
        if (chainList.Count == 0) return null;
        GameObject bottomChain = chainList[0];
        foreach (var c in chainList) if (c.transform.position.y < bottomChain.transform.position.y) bottomChain = c;
        return bottomChain;
    }

    void Activate() {
        movingUp = true;
    }

    void Deactivate() {
        movingUp = false;
    }

    void Freeze() {

    }
}
