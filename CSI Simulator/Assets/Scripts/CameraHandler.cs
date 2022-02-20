using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;

public class CameraHandler : MonoBehaviour
{
    private Camera myCamera;
    private bool takeScreenshot;
    //private int photoCount;
    private List<GameObject> roomCorners;
    private List<GameObject> photoTargets;
    private List<GameObject> fingerprints;
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

        GameObject[] photoEvidence = GameObject.FindGameObjectsWithTag("PhotoEvidence");
        GameObject[] swabEvidence = GameObject.FindGameObjectsWithTag("SwabEvidence");
        GameObject[] evidence = GameObject.FindGameObjectsWithTag("Evidence");
        GameObject[] targets = photoEvidence.Concat(swabEvidence).ToArray().Concat(evidence).ToArray();
        photoTargets = new List<GameObject>(targets);

        fingerprints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Fingerprint"));
        
        RenderPipelineManager.endCameraRendering += CheckPicture;
    }

    void Awake() 
    {
        myCamera = gameObject.GetComponent<Camera>();
    }

    void CheckPicture(ScriptableRenderContext context, Camera camera) {
        if (takeScreenshot) {
            print("picture started");
            takeScreenshot = false;
            
            /*
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/Photos/Photo" + photoCount + ".png", byteArray);
            photoCount += 1;
            */
            
            GameObject currentTarget = null;
            float currentDistance = 2000f;


            if (phase == 1) {

                foreach (GameObject target in roomCorners) {

                    if (target.GetComponent<CornerMarker>().marker == currentMarker) {

                        Vector3 targetDir = target.transform.position - gameObject.transform.position;

                        Physics.Raycast(gameObject.transform.position, targetDir, out RaycastHit hit);

                        if (hit.collider.gameObject == target) {
                            float targetAngle = Vector3.Angle(gameObject.transform.forward, targetDir);

                            if (targetAngle < 35.0f)
                                currentTarget = target;
                        }
                    }
                }

                if (currentTarget == null) {
                    Debug.Log("No valid target");
                } else {
                    Debug.Log("Photo of " + currentTarget.name + " taken!");
                    roomCorners.Remove(currentTarget);
                    currentMarker.SetActive(false);
                    currentMarker = null;
                    if (roomCorners.Count == 0) {
                        Debug.Log("All room photos taken");
                        phase = 2;
                        button2.SetActive(true);
                        board.SetActiveScreen(screen3);
                    }
                }

            } else if (phase == 2) {

                foreach (GameObject target in photoTargets) {

                    if (target.GetComponent<IsVisible>().CheckVisible()) {
                        
                        float dist = Vector3.Distance(gameObject.transform.position, target.transform.position);

                        if (dist < currentDistance) {
                            currentDistance = dist;
                            Vector3 targetDir = target.transform.position - gameObject.transform.position;

                            Physics.Raycast(gameObject.transform.position, targetDir, out RaycastHit hit);

                            if (hit.collider.gameObject == target) {
                                float targetAngle = Vector3.Angle(gameObject.transform.forward, targetDir);

                                if (targetAngle < 35.0f)
                                    currentTarget = target;
                            }
                        }
                    }
                    
                }

                if (currentTarget == null) {
                    Debug.Log("No valid target");
                } else {
                    Debug.Log("Photo of " + currentTarget.name + " taken!");
                    photoTargets.Remove(currentTarget);
                    if (photoTargets.Count == 0) {
                        Debug.Log("All Photos taken");
                        pouch.SetActive(true);
                        button3.SetActive(true);
                        board.SetActiveScreen(screen4);
                    }
                }

            } else if (phase == 3) {

                 foreach (GameObject target in fingerprints) {

                    if (target.GetComponent<Fingerprint>().isFound) {

                        float dist = Vector3.Distance(gameObject.transform.position, target.transform.position);

                        if (dist < currentDistance) {
                            currentDistance = dist;
                            Vector3 targetDir = target.transform.position - gameObject.transform.position;

                            Physics.Raycast(gameObject.transform.position, targetDir, out RaycastHit hit);

                            if (hit.collider.gameObject.name == target.name) {
                                float targetAngle = Vector3.Angle(gameObject.transform.forward, targetDir);

                                if (targetAngle < 35.0f)
                                    currentTarget = target;
                            }
                        }
                    } 
                }

                if (currentTarget == null) {
                    Debug.Log("No valid target");
                } else {
                    Debug.Log("Photo of " + currentTarget.name + " taken!");
                    fingerprints.Remove(currentTarget);
                    if (fingerprints.Count == 0) {
                        Debug.Log("All fingerprints gathered");
                        button6.SetActive(true);
                        board.SetActiveScreen(screen7);
                    }
                }
            }
            
        }
    }

    public void TakePicture() {
        takeScreenshot = true;
    }
}
