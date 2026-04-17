using UnityEngine;

public class TimescaleControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

[ContextMenu("Pause Timescale")]
    public void PauseTimescale()
    {
        SetTimescale(0f);
    }
    [ContextMenu("Resume Timescale")]
    public void ResumeTimescale()
    {
        SetTimescale(1f);
    }
    [ContextMenu("Set Timescale to Half")]
    public void SetTimescaleToHalf()
    {
        SetTimescale(0.5f);
    }
    [ContextMenu("Set Timescale to Double")]
    public void SetTimescaleToDouble()
    {
        SetTimescale(2f);
    }

    private void SetTimescale(float timescale)
    {
        Time.timeScale = timescale;
    }
}
