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
        int twoOrange = 0;
        int fourOrange = 1;
        int twoPink = 2;
        int twoBlue = 3;
        int respawnFirstBlues = 4;
        int eightOrange = 5;
        int fourPink = 6;
        int twoOrangeRoundTwo = 7;
        int eightBlue = 8;
        int eightMoreOrange = 9;
        int fourMorePink = 10;
        int eightPink = 11;
        int tenPink = 12;
        int twentyBlue = 13;

        int youShouldBeDeadByNow = 14;

        roundTimer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        waves =  new List<WaveSpawn>{
            new WaveSpawn(twoOrange, 0f, spawnPoints[0], portals[0]),
            new WaveSpawn(twoOrange, 0f, spawnPoints[3], portals[0]),

            new WaveSpawn(fourOrange, 0f, spawnPoints[0], portals[0]),
            new WaveSpawn(fourOrange, 0f, spawnPoints[1], portals[0]),
            new WaveSpawn(fourOrange, 0f, spawnPoints[2], portals[0]),
            new WaveSpawn(fourOrange, 0f, spawnPoints[3], portals[0]),

            new WaveSpawn(twoPink, 0f, spawnPoints[1], portals[1]),
            new WaveSpawn(twoPink, 0f, spawnPoints[3], portals[1]),

            new WaveSpawn(twoBlue, 0f, spawnPoints[0], portals[2]),
            new WaveSpawn(twoBlue, 0f, spawnPoints[3], portals[2]),

            new WaveSpawn(respawnFirstBlues, 0f, spawnPoints[0], portals[1]),
            new WaveSpawn(respawnFirstBlues, 0f, spawnPoints[3], portals[1]),
            new WaveSpawn(respawnFirstBlues, .25f, spawnPoints[0], portals[0]),
            new WaveSpawn(respawnFirstBlues, .25f, spawnPoints[3], portals[0]),

            new WaveSpawn(eightOrange, 0f, spawnPoints[0], portals[0]),
            new WaveSpawn(eightOrange, 0f, spawnPoints[1], portals[0]),
            new WaveSpawn(eightOrange, 0f, spawnPoints[2], portals[0]),
            new WaveSpawn(eightOrange, 0f, spawnPoints[3], portals[0]),
            new WaveSpawn(eightOrange, .25f, spawnPoints[4], portals[0]),
            new WaveSpawn(eightOrange, .25f, spawnPoints[5], portals[0]),
            new WaveSpawn(eightOrange, .25f, spawnPoints[6], portals[0]),
            new WaveSpawn(eightOrange, .25f, spawnPoints[7], portals[0]),

            new WaveSpawn(fourPink, 0f, spawnPoints[0], portals[1]),
            new WaveSpawn(fourPink, 0f, spawnPoints[2], portals[1]),
            new WaveSpawn(fourPink, .5f, spawnPoints[1], portals[1]),
            new WaveSpawn(fourPink, .5f, spawnPoints[3], portals[1]),

            new WaveSpawn(twoOrangeRoundTwo, 0f, spawnPoints[8], portals[0]),
            new WaveSpawn(twoOrangeRoundTwo, 0f, spawnPoints[9], portals[0]),

            new WaveSpawn(eightBlue, 0f, spawnPoints[0], portals[2]),
            new WaveSpawn(eightBlue, 0f, spawnPoints[1], portals[2]),
            new WaveSpawn(eightBlue, 0f, spawnPoints[2], portals[2]),
            new WaveSpawn(eightBlue, 0f, spawnPoints[3], portals[2]),
            new WaveSpawn(eightBlue, .25f, spawnPoints[4], portals[2]),
            new WaveSpawn(eightBlue, .25f, spawnPoints[5], portals[2]),
            new WaveSpawn(eightBlue, .25f, spawnPoints[6], portals[2]),
            new WaveSpawn(eightBlue, .25f, spawnPoints[7], portals[2]),

            new WaveSpawn(eightMoreOrange, 0f, spawnPoints[4], portals[0]),
            new WaveSpawn(eightMoreOrange, 0f, spawnPoints[5], portals[0]),
            new WaveSpawn(eightMoreOrange, 0f, spawnPoints[6], portals[0]),
            new WaveSpawn(eightMoreOrange, 0f, spawnPoints[7], portals[0]),
            new WaveSpawn(eightMoreOrange, .25f, spawnPoints[0], portals[0]),
            new WaveSpawn(eightMoreOrange, .25f, spawnPoints[1], portals[0]),
            new WaveSpawn(eightMoreOrange, .25f, spawnPoints[2], portals[0]),
            new WaveSpawn(eightMoreOrange, .25f, spawnPoints[3], portals[0]),

            new WaveSpawn(fourMorePink, 0f, spawnPoints[1], portals[1]),
            new WaveSpawn(fourMorePink, 0f, spawnPoints[3], portals[1]),
            new WaveSpawn(fourMorePink, .5f, spawnPoints[0], portals[1]),
            new WaveSpawn(fourMorePink, .5f, spawnPoints[2], portals[1]),

            new WaveSpawn(eightPink, 0f, spawnPoints[0], portals[1]),
            new WaveSpawn(eightPink, 0f, spawnPoints[1], portals[1]),
            new WaveSpawn(eightPink, 0f, spawnPoints[2], portals[1]),
            new WaveSpawn(eightPink, 0f, spawnPoints[3], portals[1]),
            new WaveSpawn(eightPink, .25f, spawnPoints[4], portals[1]),
            new WaveSpawn(eightPink, .25f, spawnPoints[5], portals[1]),
            new WaveSpawn(eightPink, .25f, spawnPoints[6], portals[1]),
            new WaveSpawn(eightPink, .25f, spawnPoints[7], portals[1]),

            new WaveSpawn(tenPink, 0f, spawnPoints[0], portals[1]),
            new WaveSpawn(tenPink, 0f, spawnPoints[1], portals[1]),
            new WaveSpawn(tenPink, 0f, spawnPoints[2], portals[1]),
            new WaveSpawn(tenPink, 0f, spawnPoints[3], portals[1]),
            new WaveSpawn(tenPink, 0f, spawnPoints[8], portals[1]),
            new WaveSpawn(tenPink, 0f, spawnPoints[9], portals[1]),
            new WaveSpawn(tenPink, .25f, spawnPoints[4], portals[1]),
            new WaveSpawn(tenPink, .25f, spawnPoints[5], portals[1]),
            new WaveSpawn(tenPink, .25f, spawnPoints[6], portals[1]),
            new WaveSpawn(tenPink, .25f, spawnPoints[7], portals[1]),

            new WaveSpawn(twentyBlue, 0f, spawnPoints[0], portals[2]),
            new WaveSpawn(twentyBlue, 0f, spawnPoints[1], portals[2]),
            new WaveSpawn(twentyBlue, 0f, spawnPoints[2], portals[2]),
            new WaveSpawn(twentyBlue, 0f, spawnPoints[3], portals[2]),
            new WaveSpawn(twentyBlue, 0f, spawnPoints[4], portals[2]),
            new WaveSpawn(twentyBlue, 0f, spawnPoints[5], portals[2]),
            new WaveSpawn(twentyBlue, 0f, spawnPoints[6], portals[2]),
            new WaveSpawn(twentyBlue, 0f, spawnPoints[7], portals[2]),
            new WaveSpawn(twentyBlue, 0f, spawnPoints[8], portals[2]),
            new WaveSpawn(twentyBlue, 0f, spawnPoints[9], portals[2]),
            new WaveSpawn(twentyBlue, .5f, spawnPoints[0], portals[2]),
            new WaveSpawn(twentyBlue, .5f, spawnPoints[1], portals[2]),
            new WaveSpawn(twentyBlue, .5f, spawnPoints[2], portals[2]),
            new WaveSpawn(twentyBlue, .5f, spawnPoints[3], portals[2]),
            new WaveSpawn(twentyBlue, .5f, spawnPoints[4], portals[2]),
            new WaveSpawn(twentyBlue, .5f, spawnPoints[5], portals[2]),
            new WaveSpawn(twentyBlue, .5f, spawnPoints[6], portals[2]),
            new WaveSpawn(twentyBlue, .5f, spawnPoints[7], portals[2]),
            new WaveSpawn(twentyBlue, .5f, spawnPoints[8], portals[2]),
            new WaveSpawn(twentyBlue, .5f, spawnPoints[9], portals[2]),

            new WaveSpawn(youShouldBeDeadByNow, 0f, spawnPoints[0], portals[0]),
            new WaveSpawn(youShouldBeDeadByNow, .25f, spawnPoints[1], portals[1]),
            new WaveSpawn(youShouldBeDeadByNow, .5f, spawnPoints[2], portals[2]),
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
