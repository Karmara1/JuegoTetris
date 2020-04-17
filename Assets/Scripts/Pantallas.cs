using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pantallas : MonoBehaviour
{
    bool isPaused = false;

    public void salir()
    {
        Debug.Log("Salio");
        Application.Quit();
    }

    public void menu()
    {
        SceneManager.LoadScene(0);
    }

    public void jugar()
    {
        SceneManager.LoadScene(1);
    }


    public void perder()
    {
        SceneManager.LoadScene(2);
    }


    public void pausado()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }

        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }
}
