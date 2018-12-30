using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class Avalanche : MonoBehaviour
{
    private bool retreating = false;
    private float visibleHeight;

    private Vector3 startPosition;

    void Start()
    {
        visibleHeight = Camera.main.orthographicSize - transform.localScale.y;
        startPosition = transform.position;
        FizzyoFramework.Instance.Recogniser.BreathStarted += OnBreathStarted;
        FizzyoFramework.Instance.Recogniser.BreathComplete += OnBreathEnded;
    }

    void Update()
    {
        if (retreating)
            return;

        if (/*transform.position.y - (visibleHeight + 2 * transform.localScale.y) > -3*/ transform.position.y > 4)
        {
            transform.Translate(Vector2.down * 0.3f * Time.deltaTime);
        }

        if (transform.position.y - (visibleHeight + 2 * transform.localScale.y) < 1)
        {
            float pressure = FizzyoFramework.Instance.Device.Pressure();
            transform.Translate(Vector2.up * 1.5f * pressure * Time.deltaTime);
        }
    }

    void OnBreathStarted(object sender)
    {
        
    }

    void OnBreathEnded(object sender, ExhalationCompleteEventArgs e)
    {
        if (e.BreathQuality >= 4)
        {
            StartCoroutine(Retreat());
        }
    }

    IEnumerator Retreat()
    { 
        retreating = true;
        while (transform.position.y - startPosition.y < 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, 1f * Time.deltaTime);
            yield return null;
        }
        retreating = false;
    }
}
