using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public float speed;
    private float currentSpeed;
    private Vector3 startPos;
    public float startRotation;
    private Rigidbody2D rb;
    public float lifetime = 0f;
    private CapsuleCollider2D collider;
    private SpriteRenderer renderer;
    private Timer roundTimer;
    private float roundTimeFired = 9000f;
    private bool firedThisRound = true;
    public UnityEvent onWake;


    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;
        startRotation = rb.rotation;
        collider = GetComponent<CapsuleCollider2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        roundTimer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        roundTimeFired = roundTimer.currentTime;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 forwardVec = Quaternion.AngleAxis(startRotation, Vector3.forward) * Vector2.up * currentSpeed;
        rb.MovePosition(rb.position + new Vector2(forwardVec.x, forwardVec.y));
        lifetime += Time.fixedDeltaTime;
        if (roundTimer.currentTime < roundTimeFired)
        {
            firedThisRound = false;
        }
        if (!firedThisRound && roundTimer.currentTime >= roundTimeFired)
        {
            WakeUp();
            firedThisRound = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CharacterController2D player = other.GetComponent<CharacterController2D>();
        if (player)
        {
            // bullets shouldn't collide with you when you've just fired them
            if (lifetime > .1f)
            {
                if (player.eating)
                {
                    Destroy(gameObject);
                }
                else
                {
                    other.GetComponent<Health>().Hurt(1);
                    Sleep();
                }
            }
        }
        else
        {
            Sleep();
        }
    }

    void Sleep()
    {
        currentSpeed = 0f;
        rb.position = startPos;
        rb.rotation = startRotation;
        collider.enabled = false;
        renderer.enabled = false;
        // Invoke("WakeUp", 5f);
    }

    void WakeUp()
    {
        onWake.Invoke();
        currentSpeed = speed;
        renderer.enabled = true;
        collider.enabled = true;
    }
    
}
