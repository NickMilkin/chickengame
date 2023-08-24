using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    Vector2 startPos;
    void Start()
    {
        startPos = transform.position;
    }

   private void OnTriggerEnter2D(Collider2D collision){

    if(collision.CompareTag("Obstacle")){
        Die();
    }
   }

   void Die(){
    Respawn();
   }

   void Respawn(){
    transform.position = startPos;
   }
}
