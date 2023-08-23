using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D playerbody;
    public float speed = 4.0f;
    public float acceleration = 1f;
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
            playerbody.AddForce(Vector2.up * jumpstrenght);
        }
        if (Input.GetMouseButtonDown(0)) {
            if (lastegg) {
                GameObject.Destroy(lastegg);
            }
            
            Vector2 aimdir = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            aimdir.Normalize();

            Transform newegg = Instantiate(egg, gameObject.transform.position + (Vector3)aimdir, gameObject.transform.rotation);
            Rigidbody2D eggbody = newegg.GetComponent<Rigidbody2D>();
            eggbody.velocity = playerbody.velocity;
            eggbody.AddForce(launchspeed * aimdir);
            playerbody.AddForce(-launchspeed * aimdir);
            lastegg = newegg.gameObject;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float horizontalVelocity = horizontalInput * speed;
        playerbody.velocity = new Vector2(
            Mathf.Clamp(
                horizontalVelocity,
                playerbody.velocity.x - Time.deltaTime * acceleration,
                playerbody.velocity.x + Time.deltaTime * acceleration
            ), playerbody.velocity.y
        );

        if (Mathf.Abs(horizontalInput) < 0.2) {
            playerbody.velocity = Vector2.ClampMagnitude(playerbody.velocity, playerbody.velocity.magnitude - 3f * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.F) && lastegg) {
            transform.position = lastegg.transform.position;
            playerbody.velocity = lastegg.GetComponent<Rigidbody2D>().velocity;
            
            GameObject.Destroy(lastegg);
        }
    }
}
