using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public class SetBar : MonoBehaviour
    {
        [SerializeField] private GameObject bar, repairIcon;

        public void Change(float percent) =>
            bar.GetComponent<Image>().fillAmount = percent;

        public void Repair(bool r) =>
            repairIcon.SetActive(r);
    }
}

