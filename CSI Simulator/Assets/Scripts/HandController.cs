using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{

    ActionBasedController controller;
    [SerializeField] private bool left;
    [SerializeField] private InputActionAsset actionAsset;
    public Hand hand;
    private float maxGrip;
    private float isMoving;
    private InputAction move;

    // Start is called before the first frame update
    void Start()
    {
        controller =  GetComponent<ActionBasedController>();
        if (left)
            move = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        else
            move = actionAsset.FindActionMap("XRI RightHand").FindAction("Teleport Mode Activate");
    }

    // Update is called once per frame
    void Update()
    {
        maxGrip = Mathf.Max(controller.selectAction.action.ReadValue<float>(), controller.activateAction.action.ReadValue<float>());
        hand.SetGrip(maxGrip);

        if (maxGrip == 0.0) {
            if (move.ReadValue<Vector2>() != Vector2.zero)
                isMoving = 1.0f;
            else
                isMoving = 0.0f;
            
            hand.SetPoint(isMoving);
        }
    }
}
