using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int Scenetoload;

    public void LoadScene()
    {
        SceneManager.LoadScene(Scenetoload);
    }
}
