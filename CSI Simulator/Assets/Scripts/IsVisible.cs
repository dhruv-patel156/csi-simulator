using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsVisible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckVisible() {
        Renderer objRenderer = GetComponent<Renderer>();
        if (objRenderer.isVisible)
            return true;
        else
            return false;
    }
}
