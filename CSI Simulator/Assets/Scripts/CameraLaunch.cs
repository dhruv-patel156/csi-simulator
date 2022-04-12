using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLaunch : MonoBehaviour
{
    public Transform socket;
    // Start is called before the first frame update
    void Start()
    {
        StartCam();
    }

    IEnumerator StartCam()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = socket.position;
    }
}
