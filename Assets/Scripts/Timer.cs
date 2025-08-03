using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float roundLength;
    public float currentTime = 0f;
    public int roundNumber = 0;
    public float lifetime = 0f;
    private float roundStartTime;
    public UnityEvent onNewRound;

    // Start is called before the first frame update
    void Start()
    {
        RestartTimer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        lifetime += Time.fixedDeltaTime;
        currentTime += Time.fixedDeltaTime;
        if (currentTime >= roundLength)
        {
            // Debug.Log("Round Over!");
            roundNumber++;
            onNewRound.Invoke();
            RestartTimer();
        }
    }

    private void RestartTimer()
    {
        roundStartTime = lifetime;
        currentTime = 0f;
    }
}
