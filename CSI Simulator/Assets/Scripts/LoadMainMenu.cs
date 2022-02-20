using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    public void ReturnToMain()
    {
        SceneManager.LoadScene("Start Menu", LoadSceneMode.Single);
    }
}
