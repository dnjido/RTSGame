using UnityEngine;

namespace RTS
{

    public class EnemyUI : MonoBehaviour // Health bar enemy
    {
        [SerializeField] private GameObject prefab, parent;
        [SerializeField] private GameObject UI;

        private HealthSystem health => GetComponent<HealthSystem>();
        private RepairBuild repair => GetComponent<RepairBuild>();

        void Awake()
        {
            UI = Instantiate(prefab, parent.transform, false);
        }

        void OnEnable()
        {
            if (health) health.DamageEvent += ChangeValue;
            if (repair) repair.RepairEvent += RepairIcon;
        }

        void OnDisable()
        {
            if (health) health.DamageEvent -= ChangeValue;
            if (repair) repair.RepairEvent -= RepairIcon;
        }

        void Update() =>
            UI.transform.LookAt(Camera.main.transform);

        public void ChangeValue(float c)
        {
            UI.GetComponent<SetBar>().Change(c);
        }

        public void RepairIcon(bool r)
        {
            UI.GetComponent<SetBar>().Repair(r);
        }
    }
}