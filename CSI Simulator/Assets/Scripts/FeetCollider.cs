using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    public Transform head; 
    void Update()
    {
        gameObject.transform.position = new Vector3 (head.position.x, 0.00999f, head.position.z);
    }
}
