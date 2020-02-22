using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //VARIABLES
    public Slider VolumeSlider;

    //CREAR RANDOM DE MAPA

    public void PlayGame()
    {
        SceneManager.LoadScene("IgnacioScene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void Volume()
    {
        AudioListener.volume = VolumeSlider.value;
    }
}
