using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuMusic : MonoBehaviour
{   
    public AudioSource pitch;
    public PauseMenu pauseMenu;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pauseMenu.gameIsPaused == true)
        {
            pitch.pitch = 0.7f;
        }
        else
        {
            pitch.pitch = 1f;
        }
    }
}
