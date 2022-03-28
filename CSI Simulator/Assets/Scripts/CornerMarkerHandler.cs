using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerMarkerHandler : MonoBehaviour
{
    public CameraHandler cameraHandler;
    public TeleportTutorialHandler teleportTutorial;
    public int phase;

    void Start() {
        phase = 1;
    }
    void OnTriggerEnter(Collider marker) {
        if (phase == 1)
            teleportTutorial.currentMarker = marker.gameObject;
        else if (phase == 2)
            cameraHandler.currentMarker = marker.gameObject;
    }

    void OnTriggerExit(Collider marker) {
        if (phase == 1)
            teleportTutorial.currentMarker = null;
        else if (phase == 2)
            cameraHandler.currentMarker = null;
    } 
}
