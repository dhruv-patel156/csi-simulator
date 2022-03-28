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

    public void CreateBag()
    {
        GameObject copy;
        if (leftHand.IsSelecting(pouchInteractor)) {
            copy = Instantiate(original, leftHand.transform.position, leftHand.transform.rotation);
            interactionManager.SelectExit(leftHand.GetComponent<IXRSelectInteractor>(), pouchInteractor.GetComponent<IXRSelectInteractable>());
            interactionManager.SelectEnter(leftHand.GetComponent<IXRSelectInteractor>(), copy.GetComponent<XRGrabInteractable>().GetComponent<IXRSelectInteractable>());
        }  
        else if (rightHand.IsSelecting(pouchInteractor)) {
            copy = Instantiate(original, rightHand.transform.position, rightHand.transform.rotation);
            interactionManager.SelectExit(rightHand.GetComponent<IXRSelectInteractor>(), pouchInteractor.GetComponent<IXRSelectInteractable>());
            interactionManager.SelectEnter(rightHand.GetComponent<IXRSelectInteractor>(), copy.GetComponent<XRGrabInteractable>().GetComponent<IXRSelectInteractable>());
        }
    }
}
