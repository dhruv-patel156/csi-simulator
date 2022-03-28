using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportRay : MonoBehaviour
{

    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor lRayInteractor;
    [SerializeField] private ActionBasedController lController;
    [SerializeField] private XRRayInteractor lMenuRay;
    [SerializeField] private XRRayInteractor rRayInteractor;
    [SerializeField] private ActionBasedController rController;
    [SerializeField] private XRRayInteractor rMenuRay;
    [SerializeField] private TeleportationProvider provider;
    private AudioSource teleportSound;

    public GameObject reticle;
    private InputAction lStick;
    private InputAction rStick;
    private bool lactive;
    private bool ractive;

    // Start is called before the first frame update
    void Start()
    {
        teleportSound = gameObject.GetComponent<AudioSource>();

        lRayInteractor.enabled = false;
        rRayInteractor.enabled = false;

        lMenuRay.enabled = true;
        rMenuRay.enabled = true;

        var lActivate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        lActivate.Enable();
        lActivate.performed += OnLTeleportActivate;

        var lCancel = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Cancel");
        lCancel.Enable();
        lCancel.performed += OnTeleportCancel;

        lStick = actionAsset.FindActionMap("XRI LeftHand").FindAction("Move");
        lStick.Enable();

        var rActivate = actionAsset.FindActionMap("XRI RightHand").FindAction("Teleport Mode Activate");
        rActivate.Enable();
        rActivate.performed += OnRTeleportActivate;

        var rCancel = actionAsset.FindActionMap("XRI RightHand").FindAction("Teleport Mode Cancel");
        rCancel.Enable();
        rCancel.performed += OnTeleportCancel;

        rStick = actionAsset.FindActionMap("XRI RightHand").FindAction("Move");
        rStick.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(!lactive && !ractive) return;
        
        if(lStick.ReadValue<Vector2>() != Vector2.zero || rStick.ReadValue<Vector2>() != Vector2.zero) return;

        //var lHitFail = !lRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit lhit);
        var lHitFail = !lRayInteractor.TryGetHitInfo(out Vector3 lhitPos, out Vector3 lhitNorm, out int lhitLinePos, out bool lhitValid);
        //var rHitFail = !rRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit rhit);
        var rHitFail = !rRayInteractor.TryGetHitInfo(out Vector3 rhitPos, out Vector3 rhitNorm, out int rhitLinePos, out bool rhitValid);

        if((lHitFail && rHitFail) || (!lhitValid && !rhitValid))
        {
            lRayInteractor.enabled = false;
            rRayInteractor.enabled = false;
            lMenuRay.enabled = true;
            rMenuRay.enabled = true;
            lactive = false;
            ractive = false;
            reticle.SetActive(false);
            return;
        }

        if(lactive)
        {   
            TeleportRequest lrequest = new TeleportRequest()
            {
                destinationPosition = lhitPos,
            };
            teleportSound.Play();
            provider.QueueTeleportRequest(lrequest);
            rRayInteractor.enabled = false;
            lRayInteractor.enabled = false;
            lMenuRay.enabled = true;
            rMenuRay.enabled = true;
            lactive = false;
            ractive = false;
            reticle.SetActive(false);
            return;
        }
        
        TeleportRequest rrequest = new TeleportRequest()
        {
            destinationPosition = rhitPos,
        };
        teleportSound.Play();
        provider.QueueTeleportRequest(rrequest);
        lRayInteractor.enabled = false;
        rRayInteractor.enabled = false;
        lMenuRay.enabled = true;
        rMenuRay.enabled = true;
        lactive = false;
        ractive = false;
        reticle.SetActive(false);
    }

    private void OnLTeleportActivate(InputAction.CallbackContext context)
    {
        if(!ractive && lController.selectAction.action.ReadValue<float>() == 0.0)
        {
            lRayInteractor.enabled = true;
            lMenuRay.enabled = false;
            lactive = true;
            reticle.SetActive(true);
        }  
    }

    private void OnRTeleportActivate(InputAction.CallbackContext context)
    {
        if(!lactive && rController.selectAction.action.ReadValue<float>() == 0.0)
        {
            rRayInteractor.enabled = true;
            rMenuRay.enabled = false;
            ractive = true;
            reticle.SetActive(true);
        }  
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        lRayInteractor.enabled = false;
        rRayInteractor.enabled = false;
        lMenuRay.enabled = true;
        rMenuRay.enabled = true;
        lactive = false;
        ractive = false;
        reticle.SetActive(false);
    }
}
