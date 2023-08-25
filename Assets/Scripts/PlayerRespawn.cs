using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public bool dieOnDeath = false;
    Vector2 startPos;

    public Animator transition;
    public float transitionTime = 1;
    void Start()
    {
        startPos = transform.position;
    }

   private void OnTriggerEnter2D(Collider2D collision){

    if(collision.CompareTag("Obstacle")){
        Die();
    }
   }

    void Die() {
        if (gameObject.GetComponent<PlayerMovement>()) {
            PlayerMovement player = gameObject.GetComponent<PlayerMovement>();
            player.YouDied();
            
            StartCoroutine(RespawnTransition());
        
            IEnumerator RespawnTransition(){
                SpriteRenderer chickenSprite = gameObject.GetComponent<SpriteRenderer>();
                chickenSprite.enabled = false;
                yield return new WaitForSeconds(1);
                transition.SetTrigger("Start"); //Scene change causes trigger of transistion.
                yield return new WaitForSeconds(transitionTime);
                //Player to respawn at start
                chickenSprite.enabled = true;
                Respawn();
            }
        } else {
            GameObject.Destroy(gameObject);
        }
    }

   void Respawn(){
    transform.position = startPos;
    transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

   }
}
