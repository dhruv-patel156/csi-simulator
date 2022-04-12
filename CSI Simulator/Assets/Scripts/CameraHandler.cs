using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Camera myCamera;
    private bool takeScreenshot;
    private int photoCount;
    private List<GameObject> photoTargets;

    void Start() {
        photoCount = 0;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("PhotoEvidence");
        photoTargets = new List<GameObject>(targets);
    }

    public void TakePicture() {

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
            }
        }
    }
}
