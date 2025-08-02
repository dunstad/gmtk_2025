using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGhost : Ghost
{
    private float speedStore;
    private CapsuleCollider2D collider;
    private SpriteRenderer renderer;
    private bool spawnedThisRound = true;
    private float timeOfDeath = 0f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        collider = GetComponent<CapsuleCollider2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        base.FixedUpdate();
        if (roundTimer.currentTime < timeOfDeath)
        {
            spawnedThisRound = false;
        }
        if (!spawnedThisRound && (roundTimer.currentTime >= timeOfDeath))
        {
            WakeUp();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Bullet>())
        {
            // Destroy(gameObject);
            Sleep();
        }
    }
    
    void Sleep()
    {
        timeOfDeath = roundTimer.currentTime;
        speedStore = topSpeed;
        topSpeed = 0f;
        rb.simulated = false;
        collider.enabled = false;
        renderer.enabled = false;
        // Invoke("WakeUp", 5f);
    }

    void WakeUp()
    {
        timeOfDeath = 0f;
        spawnedThisRound = true;
        topSpeed = speedStore;
        rb.simulated = true;
        renderer.enabled = true;
        collider.enabled = true;
    }
}
