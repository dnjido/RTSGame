using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public class SetBar : MonoBehaviour
    {
        [SerializeField] private GameObject bar;

        public void Change(float percent) =>
            bar.GetComponent<Image>().fillAmount = percent;
    }
}

