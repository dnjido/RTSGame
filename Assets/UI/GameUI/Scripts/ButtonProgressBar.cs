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

        private Image bar => progressBar.GetComponent<Image>();

        public void Run(Timer t)
        {
            bar.fillAmount = 1;
            InitTimer(t);
        }

        public void Ñountdown() =>
            bar.fillAmount = Mathf.InverseLerp(0, duration, timer.GetTimeLeft());

        public void Set(float c) =>
            bar.fillAmount = c;

        public void Recovery(Timer t) 
        {
            if (t == null) return;
            InitTimer(t);
            bar.fillAmount = timer.GetTimeLeft() / duration;
        }

        private void InitTimer(Timer t)
        {
            timer = t;
            duration = timer.GetDuration();
            OnEnable();
        }

        private void OnEnable()  {
            if (timer != null) timer.TickEvent += Ñountdown;
        }

        private void OnDestroy()  {
            if (timer != null) timer.TickEvent -= Ñountdown; 
        }
    }
}
