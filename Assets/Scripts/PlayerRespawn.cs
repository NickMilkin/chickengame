using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRespawn : MonoBehaviour
{
    public bool dieOnDeath = false;
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
    if (gameObject.GetComponent<PlayerMovement>()) {
        PlayerMovement.deathEvent.Invoke();
        Respawn();
    } else {
        GameObject.Destroy(gameObject);
    }
    }

   void Respawn() {
    Debug.Log("test");
    transform.position = startPos;
    transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

   }
}
