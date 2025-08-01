using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn
{
    public int roundNumber;
    public float roundTime;
    public Transform spawnPoint;
    public Portal portal;

    public WaveSpawn(int roundNum, float time, Transform point, Portal port)
    {
        roundNumber = roundNum;
        roundTime = time;
        spawnPoint = point;
        portal = port;
    }

}
