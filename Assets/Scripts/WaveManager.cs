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
            new WaveSpawn(0, .25f, spawnPoints[1], portals[1]),
            new WaveSpawn(0, .5f, spawnPoints[2], portals[2]),
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
