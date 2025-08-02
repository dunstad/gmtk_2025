using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<Portal> portals;
    public List<Transform> spawnPoints;
    private Timer roundTimer;

    private List<WaveSpawn> waves;

    // Start is called before the first frame update
    void Start()
    {
        roundTimer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        waves =  new List<WaveSpawn>{
            new WaveSpawn(0, 0f, spawnPoints[0], portals[0]),
            new WaveSpawn(0, 0f, spawnPoints[3], portals[0]),

            new WaveSpawn(1, 0f, spawnPoints[0], portals[0]),
            new WaveSpawn(1, 0f, spawnPoints[1], portals[0]),
            new WaveSpawn(1, 0f, spawnPoints[2], portals[0]),
            new WaveSpawn(1, 0f, spawnPoints[3], portals[0]),

            new WaveSpawn(2, 0f, spawnPoints[0], portals[0]),
            new WaveSpawn(2, 0f, spawnPoints[3], portals[0]),
            new WaveSpawn(2, 0f, spawnPoints[4], portals[1]),

            new WaveSpawn(3, 0f, spawnPoints[0], portals[0]),
            new WaveSpawn(3, 0f, spawnPoints[1], portals[0]),
            new WaveSpawn(3, 0f, spawnPoints[2], portals[0]),
            new WaveSpawn(3, 0f, spawnPoints[3], portals[0]),
            new WaveSpawn(3, .25f, spawnPoints[4], portals[0]),
            new WaveSpawn(3, .25f, spawnPoints[5], portals[0]),
            new WaveSpawn(3, .25f, spawnPoints[6], portals[0]),
            new WaveSpawn(3, .25f, spawnPoints[7], portals[0]),

            new WaveSpawn(4, 0f, spawnPoints[2], portals[0]),
            new WaveSpawn(4, 0f, spawnPoints[0], portals[0]),
            new WaveSpawn(4, .25f, spawnPoints[1], portals[0]),
            new WaveSpawn(4, .25f, spawnPoints[3], portals[0]),
            new WaveSpawn(4, .5f, spawnPoints[4], portals[0]),
            new WaveSpawn(4, .5f, spawnPoints[6], portals[0]),
            new WaveSpawn(4, .75f, spawnPoints[5], portals[0]),
            new WaveSpawn(4, .75f, spawnPoints[7], portals[0]),

            new WaveSpawn(5, 0f, spawnPoints[0], portals[1]),
            new WaveSpawn(5, 0f, spawnPoints[2], portals[1]),
            new WaveSpawn(5, .5f, spawnPoints[1], portals[1]),
            new WaveSpawn(5, .5f, spawnPoints[3], portals[1]),

            new WaveSpawn(6, 0f, spawnPoints[1], portals[2]),
            new WaveSpawn(6, .25f, spawnPoints[2], portals[2]),

            new WaveSpawn(7, 0f, spawnPoints[8], portals[0]),
            new WaveSpawn(7, .1f, spawnPoints[9], portals[0]),
            new WaveSpawn(7, .2f, spawnPoints[8], portals[0]),
            new WaveSpawn(7, .3f, spawnPoints[9], portals[0]),
            new WaveSpawn(7, .4f, spawnPoints[8], portals[0]),
            new WaveSpawn(7, .5f, spawnPoints[9], portals[0]),


            new WaveSpawn(10, 0f, spawnPoints[0], portals[0]),
            new WaveSpawn(10, .25f, spawnPoints[1], portals[1]),
            new WaveSpawn(10, .5f, spawnPoints[2], portals[2]),
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() {
        for (int i = 0; i < waves.Count; i++)
        {
            WaveSpawn spawn = waves[i];
            if (((roundTimer.currentTime / roundTimer.roundLength) >= spawn.roundTime) && (roundTimer.roundNumber == spawn.roundNumber))
            {
                Instantiate(spawn.portal, spawn.spawnPoint);
                waves.RemoveAt(i);
            }
        }
    }
}
