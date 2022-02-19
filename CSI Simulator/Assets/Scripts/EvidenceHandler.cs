using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EvidenceHandler : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor bagSocket;
    public GameObject storedEvidence;

    void Awake()
    {
        storedEvidence = null;
    }

    public void EvidenceStored()
    {
        BoxCollider box = gameObject.GetComponent<BoxCollider>();
        box.size = new Vector3(0.24f, 0.4f, 0.0675f);

        if (bagSocket.selectTarget.gameObject.tag == "Swab") {
            storedEvidence = bagSocket.selectTarget.gameObject.GetComponent<SwabHandler>().swabbedEvidence;
        } else {
            storedEvidence = bagSocket.selectTarget.gameObject;
        }
    }

    public void EvidenceRemoved()
    {
        BoxCollider box = gameObject.GetComponent<BoxCollider>();
        box.size = new Vector3(0.24f, 0.4f, 0.008f);

        storedEvidence = null;
    }
}
