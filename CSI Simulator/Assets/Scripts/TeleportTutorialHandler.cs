using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTutorialHandler : MonoBehaviour
{
    private List<GameObject> markers;
    public GameObject currentMarker;
    public GameObject cameraTool;
    public GameObject cameraAttach;
    public CornerMarkerHandler cornerMarkerHandler;
    public GameObject cornerMarker1;
    public GameObject cornerMarker2;
    public GameObject cornerMarker3;
    public GameObject cornerMarker4;
    public BoardMenuHandler board;
    public GameObject screen2;
    public GameObject button;

    void Start()
    {
        markers = new List<GameObject>(GameObject.FindGameObjectsWithTag("TeleportMarker"));
        currentMarker = null;
    }

    void Update()
    {
        if (currentMarker != null) {
            print(currentMarker.name + " teleported to");
            markers.Remove(currentMarker);
            currentMarker.SetActive(false);
            currentMarker = null;

            if (markers.Count == 0) {
                cornerMarkerHandler.phase = 2;
                cameraTool.SetActive(true);
                cameraAttach.SetActive(true);
                cornerMarker1.SetActive(true);
                cornerMarker2.SetActive(true);
                cornerMarker3.SetActive(true);
                cornerMarker4.SetActive(true);
                button.SetActive(true);
                board.SetActiveScreen(screen2);
            }
        }
    }
}