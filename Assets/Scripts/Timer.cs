using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float roundLength;
    public float currentTime = 0f;
    private float lifetime = 0f;
    private float roundStartTime;

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
            Debug.Log("Round Over!");
            RestartTimer();
        }
    }

    private void RestartTimer()
    {
        roundStartTime = lifetime;
        currentTime = 0f;
    }
}
