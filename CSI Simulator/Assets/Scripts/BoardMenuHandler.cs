using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMenuHandler : MonoBehaviour
{
    public GameObject activeScreen;

    public void SetActiveScreen(GameObject newActive) {
        activeScreen.SetActive(false);
        activeScreen = newActive;
        activeScreen.SetActive(true);
    }
}
