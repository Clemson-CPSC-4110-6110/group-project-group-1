using UnityEngine;
using UnityEngine.UI;

public class PourTest : MonoBehaviour
{
    public Transform bottle;
    public Transform cup;

    [Range(0f, 1f)] public float fill = 0f;
    public float fillRatePerSecond = 0.25f;
    public float pourDistance = 0.25f;
    public float tiltThresholdDot = 0.3f; // higher = easier to pour

    public Slider fillSlider;
    public GameObject fullText;

    void Start()
    {
        if (fillSlider) fillSlider.value = fill;
        if (fullText) fullText.SetActive(false);
    }

    void Update()
    {
        if (!bottle || !cup) return;

        bool closeEnough = Vector3.Distance(bottle.position, cup.position) <= pourDistance;

        // bottle is "pouring" if its top points down enough
        float downDot = Vector3.Dot((-bottle.up).normalized, Vector3.down);
        bool tiltedDown = downDot >= tiltThresholdDot;

        if (closeEnough && tiltedDown && fill < 1f)
        {
            fill += fillRatePerSecond * Time.deltaTime;
            fill = Mathf.Clamp01(fill);
        }

        if (fillSlider) fillSlider.value = fill;

        if (fullText) fullText.SetActive(fill >= 1f);
    }
}