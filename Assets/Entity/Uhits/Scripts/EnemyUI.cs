using UnityEngine;

namespace RTS
{

    public class EnemyUI : MonoBehaviour // Health bar enemy
    {
        [SerializeField] private GameObject prefab, parent;
        [SerializeField] private GameObject UI;

        void Start()
        {
            UI = Instantiate(prefab, parent.transform, false);
            GetComponent<HealthSystem>().DamageEvent += ChangeValue;
        }

        void Update() =>
            UI.transform.LookAt(Camera.main.transform);

        public void ChangeValue(float c)
        {
            UI.GetComponent<SetBar>().Change(c);
        }
    }
}