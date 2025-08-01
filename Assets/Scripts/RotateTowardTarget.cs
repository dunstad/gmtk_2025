using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardTarget : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (target)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            Vector2 targetPos = new Vector2(target.position.x, target.position.y);
            Vector2 moveVec = (targetPos - pos);
            moveVec.Normalize();
            transform.eulerAngles = (new Vector3(0, 0, 90f + Vector2.Angle(Vector2.up, moveVec) * (moveVec.x < 0 ? 1 : -1)));
            if ((transform.eulerAngles.z >= 90f && transform.eulerAngles.z <= 270f) || (transform.eulerAngles.z <= -90f))
            {
                Vector3 theScale = transform.localScale;
                theScale.y = -1;
                transform.localScale = theScale;
            }
            else
            {
                Vector3 theScale = transform.localScale;
                theScale.y = 1;
                transform.localScale = theScale;
            }
        }
    }
}
