using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsVisible : MonoBehaviour
{
    [SerializeField] private float maxDist;
    [SerializeField] private float minDist;
    public bool CheckVisible() {

        Renderer objRenderer;

        if (gameObject.tag == "SwabEvidence") {

            return true;
        
        } else {

            if (gameObject.name == "VictimPivot") {
                objRenderer = gameObject.transform.parent.Find("Body/Geo/Suit").GetComponent<Renderer>();
            } else {
                objRenderer = GetComponent<Renderer>();
            }

            if (objRenderer.isVisible)
                return true;
            else
                return false;
        }  
    }

    public bool InRange(float dist, out int error) {

        if (dist > maxDist) {
            error = 4;
            return false;
        } else if (dist < minDist) {
            error = 5;
            return false;
        } else {
            error = 0;
            return true;
        }
    }
}
