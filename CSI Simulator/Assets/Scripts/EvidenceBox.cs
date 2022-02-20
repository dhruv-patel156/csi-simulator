using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EvidenceBox : MonoBehaviour
{
    private List<GameObject> swabEvidence;
    private List<GameObject> evidence;
    public GameObject swabPouch;
    public GameObject brush;
    public GameObject brushSocket;
    public GameObject laptop;
    public CameraHandler cameraHandler;
    public BoardMenuHandler board;
    public GameObject button4;
    public GameObject button5;
    public GameObject screen5;
    public GameObject screen6;
    private int phase;

    void Start()
    {
        phase = 1;
        swabEvidence = new List<GameObject>(GameObject.FindGameObjectsWithTag("SwabEvidence"));
        evidence = new List<GameObject>(GameObject.FindGameObjectsWithTag("Evidence"));
    }

    void OnTriggerEnter(Collider collidingObject)
    {
        if (collidingObject.gameObject.tag == "EvidenceBag") {

            GameObject storedEvidence = collidingObject.gameObject.GetComponent<EvidenceHandler>().storedEvidence;

            if (storedEvidence != null) {

                Debug.Log(storedEvidence.name + " has been stored!");

                if (phase == 1)
                    evidence.Remove(storedEvidence);
                else if (phase == 2)
                    swabEvidence.Remove(storedEvidence);

                collidingObject.gameObject.SetActive(false);

                if (evidence.Count == 0) {
                    Debug.Log("All evidence stored!");
                    swabPouch.SetActive(true);
                    button4.SetActive(true);
                    board.SetActiveScreen(screen5);
                    phase = 2;
                }

                if (swabEvidence.Count == 0) {
                    Debug.Log("All swab evidence stored!");
                    brush.SetActive(true);
                    brushSocket.SetActive(true);
                    laptop.transform.Find("LaptopKeyboard").gameObject.SetActive(true);
                    laptop.transform.Find("LaptopScreen").gameObject.SetActive(true);
                    brush.transform.position = brushSocket.transform.position;
                    button5.SetActive(true);
                    board.SetActiveScreen(screen6);
                    cameraHandler.phase = 3;
                }
            }
        }
    }
}
