using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PouchHandler : MonoBehaviour
{
    [SerializeField] private XRInteractionManager interactionManager;
    [SerializeField] private XRDirectInteractor leftHand;
    [SerializeField] private XRDirectInteractor rightHand;
    [SerializeField] private XRSimpleInteractable pouchInteractor;
    [SerializeField] private GameObject original;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBag()
    {
        GameObject copy;
        if (leftHand.IsSelecting(pouchInteractor)) {
            copy = Instantiate(original, leftHand.transform.position, leftHand.transform.rotation);
            interactionManager.SelectExit(leftHand, pouchInteractor);
            interactionManager.SelectEnter(leftHand, copy.GetComponent<XRGrabInteractable>());
        }  
        else if (rightHand.IsSelecting(pouchInteractor)) {
            copy = Instantiate(original, rightHand.transform.position, rightHand.transform.rotation);
            interactionManager.SelectExit(rightHand, pouchInteractor);
            interactionManager.SelectEnter(rightHand, copy.GetComponent<XRGrabInteractable>());
        }
    }
}
