using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject door;
    public Sprite buttonDown;
    public SpriteRenderer spriteRenderer;
    private bool active = true;
    void OnTriggerEnter2D(Collider2D col) {
        if (!active || col.gameObject.tag != "Player"){
            return;
        }
        active = false;
        door.transform.position += Vector3.left * 2;
        spriteRenderer.sprite = buttonDown;
    }
}
