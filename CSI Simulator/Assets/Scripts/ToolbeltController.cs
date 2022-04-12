using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbeltController : MonoBehaviour
{

    public Transform head;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3 (head.position.x, head.position.y/2, head.position.z);
        gameObject.transform.forward = new Vector3 (head.forward.x, 0, head.forward.z);
    }
}
