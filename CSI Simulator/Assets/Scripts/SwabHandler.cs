using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SwabHandler : MonoBehaviour
{
    public GameObject swabbedEvidence;
    [SerializeField] private Material contaminatedMaterial;
    private bool contaminated;
    // Start is called before the first frame update
    void Awake()
    {
        swabbedEvidence = null;
        contaminated = false;
    }

    void OnTriggerEnter(Collider collidingObject)
    {
        if (!contaminated) {
            if (collidingObject.gameObject.tag == "SwabEvidence") {
                if (swabbedEvidence == null) {
                    swabbedEvidence = collidingObject.gameObject;
                    gameObject.transform.Find("Brush").gameObject.GetComponent<MeshRenderer>().material = swabbedEvidence.GetComponent<MeshRenderer>().material;

                    gameObject.GetComponent<XRGrabInteractable>().interactionLayers = InteractionLayerMask.GetMask("Default", "Evidence");
                } else {
                    if (collidingObject.gameObject != swabbedEvidence) {
                        contaminated = true;

                        gameObject.transform.Find("Brush").gameObject.GetComponent<MeshRenderer>().material = contaminatedMaterial;
                        gameObject.GetComponent<XRGrabInteractable>().interactionLayers = InteractionLayerMask.GetMask("Default");
                    }
                }
            }
        }
    }
}
