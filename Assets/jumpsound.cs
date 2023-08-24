using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpsound : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
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
            audioSource1.Play();
    }
      if(Input.GetMouseButtonDown(0) == true)
    {
            audioSource2.Play();
    }
    if(Input.GetKeyDown(KeyCode.F))
    {
            audioSource3.Play();
    }
    }
}