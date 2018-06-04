using UnityEngine;

public class MyTimer
{
    private float sec = 0f;
    public float Seconds
    {
        get { return sec; }
        set { sec = value; }
    }

    private bool stop = true;
    public bool Stop
    {
        get { return stop; }
    }

    public void Update()
    {
        if (!stop && sec > 0.1f) sec -= Time.deltaTime;
        else stop = true;
	}

    public bool Starting()
    {
        return stop = false;
    }

    public bool Starting(float sec)
    {
        this.sec = sec;
        return stop = false;
    }
}
