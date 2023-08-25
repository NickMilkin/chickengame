using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpsound : MonoBehaviour
{
    public AudioSource jumpSound;
    public AudioSource throwSound;
    public AudioSource hatchSound;
    public AudioSource deathSound;
    public PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown("space") && movement.IsGrounded())
	{
            jumpSound.Play();
        }
        if(Input.GetMouseButtonDown(0))
        {
                throwSound.Play();
        }
        if(Input.GetMouseButtonDown(1) && movement.lastegg)
        {
                hatchSound.Play();
        }
    }

    public void OnDeath() 
    {
        deathSound.Play();
    }
}