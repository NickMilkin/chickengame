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
    public float raycastgroundwidth = 0.35f;
    public Transform egg;
    public float heightTestPlayer;
    public Collider2D groundCollider;
    private bool hasEgg = true;
    public PlayerFollowScript enemy;
    public GameObject lastegg { get; private set; }
    private int layerMaskGround;
    // Start is called before the first frame update
    void Start()
    {
        playerbody = gameObject.GetComponent<Rigidbody2D>();
        layerMaskGround = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {

        if (IsGrounded()) {
            hasEgg = true;
            if (Input.GetButtonDown("Jump")) {
                playerbody.AddForce(Vector2.up * jumpstrenght);
            }
        }

        if (Input.GetMouseButtonDown(0) && hasEgg) {
            if (lastegg) {
                GameObject.Destroy(lastegg);
            }
            hasEgg = false;
            
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

        if (Mathf.Abs(horizontalInput) < 0.2) { // Also affects vertical velocity
            playerbody.velocity = Vector2.ClampMagnitude(playerbody.velocity, playerbody.velocity.magnitude - 3f * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.F) && lastegg) {
            enemy.OnHatchEgg();
            hasEgg = IsGrounded();
            transform.position = lastegg.transform.position;
            playerbody.velocity = lastegg.GetComponent<Rigidbody2D>().velocity;
            
            GameObject.Destroy(lastegg);
        }
    }

    private bool IsGrounded() {
        // Note that we only check for colliders on the Ground layer (we don't want to hit ourself). 
        RaycastHit2D hit = Physics2D.Raycast(groundCollider.bounds.center, Vector2.down, heightTestPlayer, layerMaskGround);
        RaycastHit2D left = Physics2D.Raycast(groundCollider.bounds.center + Vector3.left * raycastgroundwidth, Vector2.down, heightTestPlayer, layerMaskGround);
        RaycastHit2D right = Physics2D.Raycast(groundCollider.bounds.center + Vector3.right * raycastgroundwidth, Vector2.down, heightTestPlayer, layerMaskGround);
        bool isGrounded = hit.collider != null;
        isGrounded |= left.collider != null;
        isGrounded |= right.collider != null;
        // It is soo easy to make misstakes so do a lot of Debug.DrawRay calls when working with colliders...
        // Debug.DrawRay(groundCollider.bounds.center, Vector2.down * heightTestPlayer, isGrounded ? Color.green : Color.red, 0.5f);
        Debug.DrawRay(groundCollider.bounds.center + Vector3.left * raycastgroundwidth, Vector2.down * heightTestPlayer, isGrounded ? Color.green : Color.red, 0.5f);
        Debug.DrawRay(groundCollider.bounds.center, Vector2.down * heightTestPlayer, isGrounded ? Color.green : Color.red, 0.5f);
        Debug.DrawRay(groundCollider.bounds.center + Vector3.right * raycastgroundwidth, Vector2.down * heightTestPlayer, isGrounded ? Color.green : Color.red, 0.5f);
        return isGrounded;
    }

}
