using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void startButton() {
         SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
