using UnityEngine;

public class Timer
{
    public readonly float duration;
    private float timeLeft; 
    private bool pause, stop;

    public delegate void TimeOutDelegate();
    public event TimeOutDelegate TimeOutEvent;

    public delegate void TickDelegate();
    public event TickDelegate TickEvent;

    public Timer(float dur)
    {
        duration = dur;
        timeLeft = dur;
    }

    public void Tick()
    {
        if (pause || stop) return;

        timeLeft -= Time.deltaTime;
        TickEvent?.Invoke();

        if (timeLeft <= 0) Timeout();
    }

    public void SetPause(bool p) => pause = p;

    public float GetTimeLeft() => timeLeft;

    public void Timeout()
    {
        TimeOutEvent?.Invoke();
        stop = true;
    }
}
