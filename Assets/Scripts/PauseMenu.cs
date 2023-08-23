using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    //[SerializeField] GameObject pauseMenu;

    void Update (){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }
    
    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //game time is paused.
        GameIsPaused = true;
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //Game is unpaused.
        GameIsPaused = false;
    }

    public void Quit(){
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0);
    }
}
