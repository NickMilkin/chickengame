using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D playerbody;
    public float speed = 4.0f;
    public float acceleration = 1f;
    public float jumpstrenght = 4.5f;
    public float launchspeed = 12f;
    public float raycastgroundwidth = 0.35f;
    public float eggBoost = 0.4f;
    public Transform egg;
    public float heightTestPlayer;
    public Collider2D groundCollider;
    private bool hasEgg = true;
    public PlayerFollowScript enemy;
    public Animator animator;
    public Transform spriteTransform;
    public bool started = false;
    public GameObject lastegg { get; private set; }
    private int layerMaskGround;
    public Transform deathEffect;
    private Color eggColor;
    public static UnityEvent deathEvent;

    public Animator transition;
    public float transitionTime = 1;

    public SpriteRenderer selfRenderer;

    void Awake() {
        if (deathEvent == null)
            deathEvent = new UnityEvent();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerbody = gameObject.GetComponent<Rigidbody2D>();
        layerMaskGround = LayerMask.GetMask("Ground");
        RandomizeColor();
        deathEvent.AddListener(YouDied);
    }

    // Update is called once per frame
    void Update()
    {
        if (!selfRenderer.enabled) {
            return;
        }

        if (IsGrounded()) {
            hasEgg = true;
            if (Input.GetButtonDown("Jump")) {
                playerbody.AddForce(Vector2.up * jumpstrenght);
            }
            animator.SetBool("Grounded", true);
        } else {
            animator.SetBool("Grounded", false);
        }

        //Egg throwing
        if (Input.GetMouseButtonDown(0) && hasEgg) {
            started = true;
            if (lastegg) {
                GameObject.Destroy(lastegg);
            }
            if (enemy) {
                enemy.OnThrowEgg();
            }

            hasEgg = false;
            
            Vector2 aimdir = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            aimdir.Normalize();

            Transform newegg = Instantiate(egg, gameObject.transform.position + eggBoost * (Vector3)aimdir, gameObject.transform.rotation);
            // newegg.GetComponentInChildren<SpriteRenderer>().color = eggColor;
            Rigidbody2D eggbody = newegg.GetComponent<Rigidbody2D>();
            eggbody.velocity = playerbody.velocity;
            eggbody.AddForce(launchspeed * aimdir);
            playerbody.AddForce(-launchspeed * aimdir);
            lastegg = newegg.gameObject;
        }

        // Horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        started |= Mathf.Abs(horizontalInput) > 0.01;
        float targetVelocity = horizontalInput * speed;
        float adaptiveAcceleration = Time.deltaTime * acceleration / (Mathf.Abs(playerbody.velocity.x / speed) + 0.3f);
        playerbody.velocity = new Vector2(
            Mathf.MoveTowards(
                playerbody.velocity.x,
                targetVelocity,
                adaptiveAcceleration
            ), playerbody.velocity.y
        );

        // Animation code
        animator.SetFloat("Speed", Mathf.Abs(playerbody.velocity.x));
        if (playerbody.velocity.x >= 0f) {
            spriteTransform.localScale = Vector3.one;
        } else {
            spriteTransform.localScale = new Vector3(-1,1,1);
        }

        // Horizontal damping
        if (Mathf.Abs(horizontalInput) < 0.2) { // Also affects vertical velocity
            float absvel = Mathf.Abs(playerbody.velocity.x);
            playerbody.velocity = new Vector2(
                Mathf.MoveTowards(playerbody.velocity.x, 0, 3f * Time.deltaTime),
                playerbody.velocity.y
            );
        }

        // Hatching
        if (Input.GetMouseButtonDown(1) && lastegg) {
            started = true;

            if (enemy) {
                enemy.OnHatchEgg();
            }
            RandomizeColor();
            hasEgg = IsGrounded();
            transform.position = lastegg.transform.position;
            playerbody.velocity = lastegg.GetComponent<Rigidbody2D>().velocity;
            
            GameObject.Destroy(lastegg);
        }
    }

    public bool IsGrounded() {
        // Note that we only check for colliders on the Ground layer (we don't want to hit ourself). 
        RaycastHit2D hit = Physics2D.Raycast(groundCollider.bounds.center, Vector2.down, heightTestPlayer, layerMaskGround);
        RaycastHit2D left = Physics2D.Raycast(groundCollider.bounds.center + Vector3.left * raycastgroundwidth, Vector2.down, heightTestPlayer, layerMaskGround);
        RaycastHit2D right = Physics2D.Raycast(groundCollider.bounds.center + Vector3.right * raycastgroundwidth, Vector2.down, heightTestPlayer, layerMaskGround);
        bool isGrounded = hit.collider != null;
        isGrounded |= left.collider != null;
        isGrounded |= right.collider != null;
        // It is soo easy to make misstakes so do a lot of Debug.DrawRay calls when working with colliders...
        // Debug.DrawRay(groundCollider.bounds.center, Vector2.down * heightTestPlayer, isGrounded ? Color.green : Color.red, 0.5f);
        // Debug.DrawRay(groundCollider.bounds.center + Vector3.left * raycastgroundwidth, Vector2.down * heightTestPlayer, isGrounded ? Color.green : Color.red, 0.5f);
        // Debug.DrawRay(groundCollider.bounds.center, Vector2.down * heightTestPlayer, isGrounded ? Color.green : Color.red, 0.5f);
        // Debug.DrawRay(groundCollider.bounds.center + Vector3.right * raycastgroundwidth, Vector2.down * heightTestPlayer, isGrounded ? Color.green : Color.red, 0.5f);
        return isGrounded;
    }

    public void YouDied() {
        started = false;
        if (lastegg) {
            GameObject.Destroy(lastegg);
            lastegg = null;
        }
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        RandomizeColor();
        StartCoroutine(RespawnTransition());
            
        IEnumerator RespawnTransition(){
            selfRenderer.enabled = false;
            yield return new WaitForSeconds(1);
            transition.SetTrigger("Start"); //Scene change causes trigger of transistion.
            yield return new WaitForSeconds(transitionTime);
            //Player to respawn at start
            selfRenderer.enabled = true;

        }
    }

    private void RandomizeColor() {
        // selfRenderer.color = new Color(Random.Range(0.6f, 1f), Random.Range(0.8f, 1f), Random.Range(0.8f, 1f), 1f);
        // eggColor = new Color(Random.Range(0.6f, 1f), Random.Range(0.8f, 1f), Random.Range(0.8f, 1f), 1f);
    }

}
