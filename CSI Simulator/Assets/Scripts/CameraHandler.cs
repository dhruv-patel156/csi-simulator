using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
using TMPro;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Animator hudAnimator;
    [SerializeField] private AudioSource hudSound;
    [SerializeField] private Animator flashAnimator;
    [SerializeField] private EvidenceBox evidenceBox;
    [SerializeField] private TextMeshProUGUI cameraHUDError;
    public TextMeshProUGUI cameraHUD;
    private Camera myCamera;
    private bool takeScreenshot;
    private AudioSource soundEffect;
    private int photoCount;
    public int targetCount;
    private List<GameObject> roomCorners;
    private List<GameObject> photoTargets;
    public List<GameObject> fingerprints;
    public GameObject pouch;
    public GameObject button2;
    public GameObject button3;
    public GameObject button6;
    public BoardMenuHandler board;
    public GameObject screen3;
    public GameObject screen4;
    public GameObject screen7;
    public int phase;
    public GameObject currentMarker;
    
    void Start()
    {
        phase = 1;

        roomCorners = new List<GameObject>(GameObject.FindGameObjectsWithTag("RoomCorner"));
        currentMarker = null;

        targetCount = roomCorners.Count;
        cameraHUD.SetText("0/" + targetCount);

        GameObject[] photoEvidence = GameObject.FindGameObjectsWithTag("PhotoEvidence");
        GameObject[] swabEvidence = GameObject.FindGameObjectsWithTag("SwabEvidence");
        GameObject[] evidence = GameObject.FindGameObjectsWithTag("Evidence");
        GameObject[] targets = photoEvidence.Concat(swabEvidence).ToArray().Concat(evidence).ToArray();
        photoTargets = new List<GameObject>(targets);

        fingerprints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Fingerprint"));

        soundEffect = gameObject.GetComponent<AudioSource>();
        
        RenderPipelineManager.endCameraRendering += CheckPicture;
    }

    void Awake() 
    {
        myCamera = gameObject.GetComponent<Camera>();
    }

    void CheckPicture(ScriptableRenderContext context, Camera camera) {
        if (takeScreenshot) {
            takeScreenshot = false;

            flashAnimator.Play("Flash", 0);
            soundEffect.Play();
            
            GameObject currentTarget = null;
            float currentDistance = 2000f;
            int error = 0;
            string errorMsg = "";

            if (phase == 1) {

                foreach (GameObject target in roomCorners) {

                    if (error < 1)
                        error = 1;
                    if (target.GetComponent<CornerMarker>().marker == currentMarker) {

                        Vector3 targetDir = target.transform.position - gameObject.transform.position;

                        Physics.Raycast(gameObject.transform.position, targetDir, out RaycastHit hit);

                        if (error < 2)
                            error = 2;
                        if (hit.collider.gameObject == target) {
                            float targetAngle = Vector3.Angle(gameObject.transform.forward, targetDir);

                            if (error < 3)
                                error = 3;
                            if (targetAngle < 35.0f)
                                currentTarget = target;
                        }
                    }
                }

                if (currentTarget == null) {
                    Debug.Log("No valid target");
                    switch(error) {
                        case 1:
                            errorMsg = "Stand on correct marker";
                            break;
                        case 2:
                            errorMsg = "View is obstructed";
                            break;
                        default:
                            errorMsg = "Opposite corner not in view";
                            break;
                    }
                    cameraHUDError.SetText(errorMsg);
                    cameraHUDError.color = new Color32(255, 50, 50, 190);
                } else {
                    Debug.Log("Photo of " + currentTarget.name + " taken!");
                    cameraHUDError.SetText("Nice Photo!");
                    cameraHUDError.color = new Color32(50, 255, 50, 190);
                    roomCorners.Remove(currentTarget);
                    currentMarker.SetActive(false);
                    currentMarker = null;
                    photoCount = targetCount - roomCorners.Count;
                    cameraHUD.SetText(photoCount + "/" + targetCount);
                    if (roomCorners.Count == 0) {
                        Debug.Log("All room photos taken");
                        targetCount = photoTargets.Count;
                        cameraHUD.SetText("0/" + targetCount);
                        phase = 2;
                        button2.SetActive(true);
                        board.SetActiveScreen(screen3);
                        hudAnimator.Play("HUD1", 0);
                        hudSound.Play();
                    }
                }

            } else if (phase == 2) {

                foreach (GameObject target in photoTargets) {

                    if (error < 1)
                        error = 1;
                    if (target.GetComponent<IsVisible>().CheckVisible()) {
                        
                        float dist = Vector3.Distance(gameObject.transform.position, target.transform.position);

                        if (dist < currentDistance) {
                            currentDistance = dist;
                            Vector3 targetDir = target.transform.position - gameObject.transform.position;

                            Physics.Raycast(gameObject.transform.position, targetDir, out RaycastHit hit);

                            if (error < 2)
                                error = 2;
                            if (hit.collider.gameObject == target) {
                                float targetAngle = Vector3.Angle(gameObject.transform.forward, targetDir);

                                if (error < 3)
                                    error = 3;
                                if (targetAngle < 35.0f) {
                                    
                                    if (target.GetComponent<IsVisible>().InRange(dist, out error))
                                        currentTarget = target;
                                }
                            }
                        }
                    }
                    
                }

                if (currentTarget == null) {
                    Debug.Log("No valid target");
                    switch(error) {
                        case 1:
                            errorMsg = "No visible target";
                            break;
                        case 2:
                            errorMsg = "Target is obstructed";
                            break;
                        case 3:
                            errorMsg = "Target not in frame";
                            break;
                        case 4:
                            errorMsg = "Too far from target";
                            break;
                        default:
                            errorMsg = "Too close to target";
                            break;
                    }
                    cameraHUDError.SetText(errorMsg);
                    cameraHUDError.color = new Color32(255, 50, 50, 190);
                } else {
                    Debug.Log("Photo of " + currentTarget.name + " taken!");
                    cameraHUDError.SetText("Nice Photo!");
                    cameraHUDError.color = new Color32(50, 255, 50, 190);
                    photoTargets.Remove(currentTarget);
                    photoCount = targetCount - photoTargets.Count;
                    cameraHUD.SetText(photoCount + "/" + targetCount);
                    if (photoTargets.Count == 0) {
                        Debug.Log("All Photos taken");
                        pouch.SetActive(true);
                        button3.SetActive(true);
                        evidenceBox.transform.Find("Lid/Canvas").gameObject.SetActive(true);
                        evidenceBox.boxHUD1.SetText("0/" + evidenceBox.evidence.Count);
                        board.SetActiveScreen(screen4);
                        hudAnimator.Play("HUD1", 0);
                        hudSound.Play();
                    }
                }

            } else if (phase == 3) {

                 foreach (GameObject target in fingerprints) {
                    
                    if (error < 1)
                        error = 1;
                    if (target.GetComponent<Fingerprint>().isFound) {

                        float dist = Vector3.Distance(gameObject.transform.position, target.transform.position);

                        if (dist < currentDistance) {
                            currentDistance = dist;
                            Vector3 targetDir = target.transform.position - gameObject.transform.position;

                            Physics.Raycast(gameObject.transform.position, targetDir, out RaycastHit hit);

                            if (error < 2)
                                error = 2;
                            if (hit.collider.gameObject.name == target.name) {
                                float targetAngle = Vector3.Angle(gameObject.transform.forward, targetDir);

                                if (error < 3)
                                    error = 3;
                                if (targetAngle < 35.0f) {
                                    
                                    if (target.GetComponent<Fingerprint>().InRange(dist, out error))
                                        currentTarget = target;
                                }        
                            }
                        }
                    } 
                }

                if (currentTarget == null) {
                    Debug.Log("No valid target");
                    switch(error) {
                        case 1:
                            errorMsg = "No visible target";
                            break;
                        case 2:
                            errorMsg = "Target is obstructed";
                            break;
                        case 3:
                            errorMsg = "Target not in frame";
                            break;
                        default:
                            errorMsg = "Too far from target";
                            break;
                    }
                    cameraHUDError.SetText(errorMsg);
                    cameraHUDError.color = new Color32(255, 50, 50, 190);
                } else {
                    Debug.Log("Photo of " + currentTarget.name + " taken!");
                    cameraHUDError.SetText("Nice Photo!");
                    cameraHUDError.color = new Color32(50, 255, 50, 190);
                    fingerprints.Remove(currentTarget);
                    photoCount = targetCount - fingerprints.Count;
                    cameraHUD.SetText(photoCount + "/" + targetCount);
                    if (fingerprints.Count == 0) {
                        Debug.Log("All fingerprints gathered");
                        button6.SetActive(true);
                        board.SetActiveScreen(screen7);
                        hudAnimator.Play("HUD1", 0);
                        hudSound.Play();
                    }
                }
            }
            
        }
    }

    public void TakePicture() {
        takeScreenshot = true;
    }
}
