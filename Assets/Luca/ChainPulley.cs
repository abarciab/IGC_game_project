using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainPulley : MonoBehaviour
{
    public GameObject pulleyTop;
    public GameObject pulleyBottom;
    public GameObject primaryChainsWrapper;
    public GameObject pulleyLinkPrefab;

    [SerializeField]
    float chainMoveSpeed = 0.05f;    
    [SerializeField]
    float chainLength = 5f;
    [SerializeField]
    float chainCount = 0;

    private List<GameObject> chainList = new List<GameObject>();
    int nameCounter = 0;
    IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        InitChains();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitChains() {
        Vector3 spawnLocation = new Vector3(0, pulleyTop.transform.localPosition.y - 0.1f, 0);
        while(spawnLocation.y - 2.5f > pulleyBottom.transform.localPosition.y + 0.1f) {
            GameObject newChain = GameObject.Instantiate(pulleyLinkPrefab, primaryChainsWrapper.transform);
            newChain.name = "chain" + nameCounter;
            nameCounter++;
            newChain.transform.localPosition = spawnLocation;
            spawnLocation.y -= 2.5f;
        }
        foreach (Transform child in primaryChainsWrapper.transform) {
            chainList.Add(child.gameObject);
        }
    }

    IEnumerator MovePrimaryChainsUp() {
        while(true) {
            primaryChainsWrapper.transform.localPosition = new Vector3(primaryChainsWrapper.transform.localPosition.x, primaryChainsWrapper.transform.localPosition.y + chainMoveSpeed, primaryChainsWrapper.transform.localPosition.z);
            if(chainList[0].transform.position.y >= pulleyTop.transform.position.y){
                chainCount--;
                if (-chainCount == chainLength) { break; }
                Destroy(chainList[0]);
                chainList.RemoveAt(0);
                GameObject newChain = GameObject.Instantiate(pulleyLinkPrefab, primaryChainsWrapper.transform);
                newChain.transform.localPosition = new Vector3(0, chainList[chainList.Count - 1].transform.localPosition.y - 2.5f, 0);
                chainList.Add(newChain);
                newChain.name = "chain" + nameCounter;
                nameCounter++;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    IEnumerator MovePrimaryChainsDown() {
        while(true) {
            primaryChainsWrapper.transform.localPosition = new Vector3(primaryChainsWrapper.transform.localPosition.x, primaryChainsWrapper.transform.localPosition.y - chainMoveSpeed * 3, primaryChainsWrapper.transform.localPosition.z);
            if(chainList[chainList.Count - 1].transform.position.y <= pulleyBottom.transform.position.y){
                chainCount++;
                if (chainCount == chainLength) { break; }
                Destroy(chainList[chainList.Count - 1]);
                chainList.RemoveAt(chainList.Count - 1);
                GameObject newChain = GameObject.Instantiate(pulleyLinkPrefab, primaryChainsWrapper.transform);
                newChain.transform.localPosition = new Vector3(0, chainList[0].transform.localPosition.y + 2.5f, 0);
                chainList.Insert(0, newChain);
                newChain.name = "chain" + nameCounter;
                nameCounter++;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }


    void Activate() {
        if (coroutine != null) { StopCoroutine(coroutine); }
        coroutine = MovePrimaryChainsUp();
        StartCoroutine(coroutine);
    }

    void Deactivate() {
        if (coroutine != null) { StopCoroutine(coroutine); }
        coroutine = MovePrimaryChainsDown();
        StartCoroutine(coroutine);
    }

    void Freeze() {

    }
}
