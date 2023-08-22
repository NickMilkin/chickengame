using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D playerbody;
    public float speed = 4.0f;
    public float jumpstrenght = 4.5f;
    public float launchspeed = 12f;
    public Transform egg;
    private GameObject lastegg;
    // Start is called before the first frame update
    void Start()
    {
        playerbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            Debug.Log("Jump");
            playerbody.AddForce(Vector2.up * jumpstrenght);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (lastegg) {
                GameObject.Destroy(lastegg);
            }
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (direction.sqrMagnitude < 0.1) {
                direction = Vector3.down;
            }

            direction.Normalize();


            Transform newegg = Instantiate(egg, gameObject.transform.position + direction, gameObject.transform.rotation);
            Rigidbody2D eggbody = newegg.GetComponent<Rigidbody2D>();
            eggbody.velocity = playerbody.velocity;
            eggbody.AddForce(launchspeed * (Vector2)direction);
            playerbody.AddForce(-launchspeed * (Vector2)direction);
            lastegg = newegg.gameObject;
        }
        if (Input.GetKeyDown(KeyCode.F) && lastegg) {
            transform.position = lastegg.transform.position;
            GameObject.Destroy(lastegg);
        }
    }

    void FixedUpdate() {
        // Time.deltaTime;
        float horizontalInput = Input.GetAxis("Horizontal");
        playerbody.velocity += Vector2.right * horizontalInput * speed;
    }
}
