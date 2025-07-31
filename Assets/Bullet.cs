using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Vector3 startPos;
    public float startRotation;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;
        startRotation = rb.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 forwardVec = Quaternion.AngleAxis(startRotation, Vector3.forward) * Vector2.up * speed;
        rb.MovePosition(rb.position + new Vector2 (forwardVec.x, forwardVec.y));
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
    
}
