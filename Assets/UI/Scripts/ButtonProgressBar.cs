using System;
using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public class ButtonProgressBar : MonoBehaviour // Displays the unit creation process.
    {
        [SerializeField] private GameObject progressBar;
        private Timer timer;
        private float duration;

        public void Run(Timer t)
        {
            progressBar.GetComponent<Image>().fillAmount = 1;
            InitTimer(t);
        }

        public void Ñountdown() =>
            progressBar.GetComponent<Image>().fillAmount = Mathf.InverseLerp(0, duration, timer.GetTimeLeft());

        public void Set(float c) =>
            progressBar.GetComponent<Image>().fillAmount = c;

        public void Recovery(Timer t) 
        {
            if (t == null) return;
            InitTimer(t);
            progressBar.GetComponent<Image>().fillAmount = timer.GetTimeLeft() / duration;
        }

        private void InitTimer(Timer t)
        {
            timer = t;
            duration = timer.GetDuration();
            t.TickEvent += Ñountdown;
        }

        private void OnDestroy()  {
            if (timer != null) timer.TickEvent -= Ñountdown; 
        }
    }
}
