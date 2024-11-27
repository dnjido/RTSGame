using TMPro;
using UnityEngine;

namespace RTS
{
    public class ButtonCounter : MonoBehaviour
    {
        [SerializeField] private GameObject counter, panel;
        public int count;

        private void Start() =>
            SetAlpha();

        public void SetCount(int c)
        {
            count = c;
            counter.GetComponent<TMP_Text>().text = count.ToString();
            SetAlpha();
        }

        public void SetAlpha() =>
            panel.GetComponent<CanvasGroup>().alpha = count > 0 ? 1 : 0;
    }
}
