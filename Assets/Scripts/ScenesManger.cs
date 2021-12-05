using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadGame", 2f);
    }

    void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
