using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CameraLaunch : MonoBehaviour
{
    public Transform socket;

    void Start()
    {
        StartCoroutine(WaitForLoad());
    }

    IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(0.1f);

        gameObject.transform.position = socket.position;
    }
    
}
