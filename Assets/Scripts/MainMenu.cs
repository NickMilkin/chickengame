using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStartButton(){
        SceneManager.LoadScene(1); //loads into scene 1 (Start of game)
    }

    public void OnExitButton (){
        Application.Quit(); //Will quit the application (when built).
    }
}
