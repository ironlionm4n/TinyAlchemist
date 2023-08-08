using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void HandlePlayClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
