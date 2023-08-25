using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorTrigger : MonoBehaviour
{
    public GameObject door;
    public Sprite buttonUp;
    public Sprite buttonDown;
    public SpriteRenderer spriteRenderer;
    private bool active = true;
    public AudioSource ClickButton;

    void Start() {
        PlayerMovement.deathEvent.AddListener(Reset);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (!active || col.gameObject.tag != "Player"){
            return;
        }
        active = false;
        door.transform.position += Vector3.left * 2; //Needs changed per scene.
        spriteRenderer.sprite = buttonDown;
        ClickButton.Play();
    }

    private void Reset() {
        if (!active) {
            active = true;
            door.transform.position += Vector3.right * 2; //Needs changed per scene.
            spriteRenderer.sprite = buttonUp;
        }
    }
}
