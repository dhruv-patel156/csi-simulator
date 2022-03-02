using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class EvidenceBox : MonoBehaviour
{
    [SerializeField] private Animator hudAnimator;
    [SerializeField] private AudioSource hudSound;
    [SerializeField] private TextMeshProUGUI boxHUD2;
    public TextMeshProUGUI boxHUD1;
    private List<GameObject> swabEvidence;
    public List<GameObject> evidence;
    public GameObject swabPouch;
    private int evidenceCount;
    private int targetCount;
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
        targetCount = evidence.Count;
    }

    void OnTriggerEnter(Collider collidingObject)
    {
        if (collidingObject.gameObject.tag == "EvidenceBag") {

            GameObject storedEvidence = collidingObject.gameObject.GetComponent<EvidenceHandler>().storedEvidence;

            if (storedEvidence != null) {

                Debug.Log(storedEvidence.name + " has been stored!");

                if (phase == 1) {
                    evidence.Remove(storedEvidence);
                    evidenceCount = targetCount - evidence.Count;
                    boxHUD1.SetText(evidenceCount + "/" + targetCount);

                    if (evidence.Count == 0) {
                        Debug.Log("All evidence stored!");
                        swabPouch.SetActive(true);
                        button4.SetActive(true);
                        board.SetActiveScreen(screen5);
                        phase = 2;
                        targetCount = swabEvidence.Count;
                        boxHUD1.SetText("0/" + targetCount);
                        boxHUD2.SetText("Swabbed evidence");
                        hudAnimator.Play("HUD1", 0);
                        hudSound.Play();
                    }
                } else if (phase == 2) {
                    swabEvidence.Remove(storedEvidence);
                    evidenceCount = targetCount - swabEvidence.Count;
                    boxHUD1.SetText(evidenceCount + "/" + targetCount);

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
                        cameraHandler.targetCount = cameraHandler.fingerprints.Count;
                        cameraHandler.cameraHUD.SetText("0/" + cameraHandler.targetCount);
                        hudAnimator.Play("HUD1", 0);
                        hudSound.Play();
                    }
                }

                collidingObject.gameObject.SetActive(false);
            }
        }
    }
}
