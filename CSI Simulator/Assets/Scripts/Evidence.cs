using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Evidence : MonoBehaviour
{
    [SerializeField] private XRInteractionManager interactionManager;
    [SerializeField] private XRGrabInteractable itemInteractable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Socketed()
    {
        if (itemInteractable.selectingInteractor.GetType() == typeof(XRSocketInteractor)) {
            if (gameObject.tag == "Swab") {
                MeshRenderer[] childRenderers = GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer childRenderer in childRenderers) {
                    childRenderer.enabled = false;
                }

                Collider[] childColliders = GetComponentsInChildren<Collider>();
                foreach (Collider childCollider in childColliders) {
                    childCollider.enabled = false;
                }
            } else {
                MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
                Collider collider = gameObject.GetComponent<Collider>();

                mesh.enabled = false;
                collider.enabled = false;
            }
        }
    }
}
