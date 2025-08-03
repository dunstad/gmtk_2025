using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RestartButton : MonoBehaviour
{
    public TMP_Text score;
    private GameObject player;
    private Vector3 playerStartPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerStartPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        foreach (Ghost ghost in FindObjectsOfType<Ghost>())
        {
            Destroy(ghost.gameObject);
        }
        foreach (Bullet bullet in FindObjectsOfType<Bullet>())
        {
            Destroy(bullet.gameObject);
        }
        foreach (Portal portal in FindObjectsOfType<Portal>())
        {
            Destroy(portal.gameObject);
        }
        score.text = "0";
        player.SetActive(true);
        player.transform.position = playerStartPos;
        player.GetComponent<Health>().currentHealth = 3;
        CharacterController2D cc = player.GetComponent<CharacterController2D>();
        cc.ammo = cc.maxAmmo;
        cc.lastReloadedRoundNum = 0;

        foreach (GameObject bullet in cc.bullets)
        {
            bullet.SetActive(true);
        }
        foreach (GameObject heart in cc.hearts)
        {
            heart.SetActive(true);
        }

        Timer timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        timer.currentTime = 0f;
        timer.roundNumber = 0;
        timer.lifetime = 0f;

        WaveManager waves = GameObject.FindWithTag("WaveManager").GetComponent<WaveManager>();
        waves.Start();
    }
}
