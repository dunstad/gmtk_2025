using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGhost : Ghost
{
    private Timer roundTimer;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        roundTimer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
