using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public AudioSource ClickButton;
    //[SerializeField] GameObject pauseMenu;

    void Update (){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }
    
    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //game time is paused.
        gameIsPaused = true;
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //Game is unpaused.
        gameIsPaused = false;
        Debug.Log("Im here");
        ClickButton.Play();
    }

    public void Quit(){
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0);
        ClickButton.Play();
    }
}
