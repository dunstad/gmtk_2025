using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class Ghost : MonoBehaviour
{
    [field: SerializeField] public float topSpeed { get; set; }
    [field: SerializeField] public float acceleration { get; set; }
    protected Rigidbody2D rb;
    protected GameObject target;
    public UnityEvent onDeath;
    public UnityEvent onDoubleScoreDeath;
    private TMP_Text score;
    protected Timer roundTimer;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!target)
        {
            target = GameObject.FindWithTag("Player");
            score = GameObject.FindWithTag("Score").GetComponent<TMP_Text>();
            roundTimer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        }
    }

    protected void FixedUpdate()
    {
        Move();
    }

    protected Vector3 calcVelocity(Vector3 targetDirection)
    {
        Vector3 velocity = targetDirection * acceleration * rb.mass;
        return velocity;
    }

    protected virtual Vector3 getTargetDirection()
    {
        return (target.transform.position - transform.position).normalized;
    }

    protected void Move()
    {
        if (!target)
        {
            target = GameObject.FindWithTag("Player");
        }
        Debug.Log("moving");
        Vector3 step = calcVelocity(getTargetDirection()) * Time.deltaTime; // calculate distance to move
        rb.AddForce(step, ForceMode2D.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, topSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        if (bullet)
        {
            int scoreNum = Int32.Parse(score.text);
            int deathScore = 10;
            if (bullet.lifetime >= roundTimer.roundLength)
            {
                deathScore *= 2;
                onDoubleScoreDeath.Invoke();
            }
            else
            {
                onDeath.Invoke();
            }
            score.text = "" + (scoreNum + deathScore);
            // Destroy(gameObject);
            // gameObject.SetActive(false);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
            Invoke("Die", 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Health hp = other.gameObject.GetComponent<Health>();
        if (hp)
        {
            hp.Hurt(1);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
