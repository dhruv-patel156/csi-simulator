using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BrushHandler : MonoBehaviour
{
    public Transform socket;
    [SerializeField] private GameObject dustDecal;
    [SerializeField] private Material foundPrint;

    private List<GameObject> fingerprints;
    void Start()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = socket.position;
        fingerprints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Fingerprint"));
    }

    void OnTriggerEnter(Collider surface)
    {
        if (surface.gameObject.tag == "Surface") {
            Vector3 location = surface.ClosestPoint(gameObject.transform.position);
            int layerMask = 1 << 7;
            layerMask = ~layerMask;
            Vector3 direction = location - gameObject.transform.position;
            if (Physics.Raycast(gameObject.transform.position, direction, out RaycastHit hit, 100f, layerMask)) {
                Vector3 forward = new Vector3(-hit.normal.x, -hit.normal.y, -hit.normal.z);
                Instantiate(dustDecal, location, Quaternion.LookRotation(forward, Vector3.forward));
            }

            GameObject finger = null;
            foreach (GameObject fingerprint in fingerprints) {
                if (Vector3.Distance(fingerprint.transform.position, location) <= 0.06f) {
                    Vector3 dir = fingerprint.transform.position - gameObject.transform.position;
                    if (Physics.Raycast(gameObject.transform.position, dir, out RaycastHit hit2, 100f, layerMask)) {
                        fingerprint.transform.Find("Decal").gameObject.GetComponent<DecalProjector>().material = foundPrint;
                        finger = fingerprint;
                        break;
                    }
                }
            }

            if (finger != null) {
                finger.GetComponent<Fingerprint>().isFound = true;
                fingerprints.Remove(finger);
            }
        }
    }
}
