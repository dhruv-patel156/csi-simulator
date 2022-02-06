using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraHandler : MonoBehaviour
{
    private Camera myCamera;
    private bool takeScreenshot;
    private int photoCount;
    private List<GameObject> photoTargets;

    void Start() {
        photoCount = 0;
        GameObject[] photoEvidence = GameObject.FindGameObjectsWithTag("PhotoEvidence");
        GameObject[] evidence = GameObject.FindGameObjectsWithTag("Evidence");
        GameObject[] targets = photoEvidence.Concat(evidence).ToArray();
        photoTargets = new List<GameObject>(targets);
    }

    private void Awake() {
        myCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender() {
        if (takeScreenshot) {
            takeScreenshot = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/Photos/Photo" + photoCount + ".png", byteArray);
            photoCount += 1;

            GameObject currentTarget = null;
            float currentDistance = 2000f;

            foreach (GameObject target in photoTargets) {

                if(target.GetComponent<IsVisible>().CheckVisible()) {

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
                photoTargets.Remove(currentTarget);
                if (photoTargets.Count == 0) {
                    Debug.Log("All Photos taken");
                    GameObject.Find("XR Origin/Toolbelt/Pouch").SetActive(true);
                    GameObject.Find("XR Origin/Toolbelt/SwabPouch").SetActive(true);
                }
            }
        }
    }

    public void TakePicture() {
        takeScreenshot = true;
    }
}
