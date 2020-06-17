using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicket : MonoBehaviour
{
    [SerializeField] public new Light light;
    [SerializeField] public float minIntensity = 0f;
    [SerializeField] public float maxIntensity = 1f;
    [SerializeField] float lastSum = 0;

    [Range(1, 50)] public int smoothing = 5;
    Queue<float> smoothQueue;

    public void Reset()
    {
        smoothQueue.Clear();
        lastSum = 0;
    }

    void Start()
    {
        smoothQueue = new Queue<float>(smoothing);
        // External or internal light?
        if (light == null)
        {
            light = GetComponent<Light>();
        }
    }

    void Update()
    {
        if (light == null)
            return;

        // pop off an item if too big
        while (smoothQueue.Count >= smoothing)
        {
            lastSum -= smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        // Calculate new smoothed average
        light.intensity = lastSum / (float)smoothQueue.Count;
    }

}
