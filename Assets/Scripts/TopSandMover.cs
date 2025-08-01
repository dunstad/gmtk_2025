using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSandMover : MonoBehaviour
{
    private float sandHeight;
    private Timer timer;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        sandHeight = gameObject.GetComponent<RectTransform>().rect.height;
        timer = GetComponentInParent<Timer>();
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float timePercent = timer.currentTime <= 0 ? 0f : (float)timer.currentTime / timer.roundLength;
        transform.localPosition = startPos + new Vector3(0f, -(sandHeight * timePercent), 0f);
    }
}
