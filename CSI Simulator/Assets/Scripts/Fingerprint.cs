using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fingerprint : MonoBehaviour
{
    public bool isFound;
    void Start()
    {
        isFound = false;
    }

    public bool InRange(float dist, out int error) {

        if (dist > 0.15) {
            error = 4;
            return false;
        } else {
            error = 0;
            return true;
        }
    }
}
