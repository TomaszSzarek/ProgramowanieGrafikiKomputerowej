using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour
{
    public void ResetScene()
    {
        SceneManager.LoadScene(2);
    }
}
