using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject ghost;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnGhost", 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnGhost()
    {
        Instantiate(ghost, transform.position, transform.rotation);
        Invoke("Die", 1f);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
