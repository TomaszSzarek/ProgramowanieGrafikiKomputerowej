using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    public void CloseGame()
    {
        Debug.Log("Application is quitting");
        Application.Quit();
    }
}
