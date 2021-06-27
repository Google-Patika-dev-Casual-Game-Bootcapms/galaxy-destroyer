using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void Scene(int sceneLoad)
    {
        SceneManager.LoadScene(sceneLoad);
    }
}
