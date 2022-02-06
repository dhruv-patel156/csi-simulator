using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EvidenceBox : MonoBehaviour
{
    private List<GameObject> boxTargets;

    void Start()
    {
        GameObject[] physicalEvidence = GameObject.FindGameObjectsWithTag("PhysicalEvidence");
        GameObject[] swabEvidence = GameObject.FindGameObjectsWithTag("SwabEvidence");
        GameObject[] evidence = GameObject.FindGameObjectsWithTag("Evidence");
        GameObject[] targets = physicalEvidence.Concat(swabEvidence).ToArray().Concat(evidence).ToArray();
        boxTargets = new List<GameObject>(targets);
    }

    void OnTriggerEnter(Collider collidingObject)
    {
        if (collidingObject.gameObject.tag == "EvidenceBag") {
            GameObject storedEvidence = collidingObject.gameObject.GetComponent<EvidenceHandler>().storedEvidence;
            if (storedEvidence != null) {
                Debug.Log(storedEvidence.name + " has been stored!");
                boxTargets.Remove(storedEvidence);
                collidingObject.gameObject.SetActive(false);
                if (boxTargets.Count == 0) {
                    Debug.Log("All evidence stored!");
                }
            }
        }
    }
}
