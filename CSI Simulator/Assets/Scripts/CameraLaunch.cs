using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLaunch : MonoBehaviour
{
    public Transform socket;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = socket.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
