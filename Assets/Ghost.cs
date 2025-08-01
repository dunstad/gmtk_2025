using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [field: SerializeField] public float topSpeed { get; set; }
    [field: SerializeField] public float acceleration { get; set; }
    private Rigidbody2D rb;
    private GameObject target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!target)
        {
            target = GameObject.FindWithTag("Player");
        }
    }

    void FixedUpdate() {
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
        Vector3 step =  calcVelocity(getTargetDirection()) * Time.deltaTime; // calculate distance to move
        rb.AddForce(step, ForceMode2D.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, topSpeed);
    }
}
