using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public class ButtonProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject progressBar;
        private Timer timer;

        public void Run(Timer t)
        {
            timer = t;
            progressBar.GetComponent<Image>().fillAmount = 1;
            timer.TickEvent += Ñountdown;
        }

        public void Ñountdown() =>
            progressBar.GetComponent<Image>().fillAmount = Mathf.InverseLerp(0, timer.duration, timer.GetTimeLeft());
    }
}
