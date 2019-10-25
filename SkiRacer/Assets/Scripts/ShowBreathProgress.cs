using Fizzyo;
using UnityEngine;
using UnityEngine.UI;

public class ShowBreathProgress : MonoBehaviour
{
    public Image ProgressEllipse;

    private float startTime = 0;
    private bool exhaling = false;
    private float progressAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        FizzyoFramework.Instance.Recogniser.BreathStarted += OnBreathStarted;
        FizzyoFramework.Instance.Recogniser.BreathComplete += OnBreathEnded;
    }

    // Update is called once per frame
    void Update()
    {
        if (exhaling)
        {
            float exhaleTime = (Time.realtimeSinceStartup - startTime);
            float progress = exhaleTime / FizzyoFramework.Instance.Device.maxPressureCalibrated;

            progressAmount = Mathf.Min(progress, 1.0f);
        }
    }

    private void LateUpdate()
    {
        ProgressEllipse.fillAmount = progressAmount;
    }


    void OnBreathStarted(object sender)
    {
        startTime = Time.realtimeSinceStartup;
        exhaling = true;
    }

    void OnBreathEnded(object sender, ExhalationCompleteEventArgs e)
    {
        exhaling = false;
        ProgressEllipse.fillAmount = 0f;
    }
}
