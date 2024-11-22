using UnityEngine;
using Zenject;

namespace RTS
{
    public class BuildUnit : MonoBehaviour
    {
        [SerializeField] private GameObject point;
        private GameObject unit;
        [SerializeField] private float time;
        UnitFacade.Factory unitFactory;
        private Timer timer;


        [Inject]
        public void ProjectileFactory(UnitFacade.Factory f)
        {
            unitFactory = f;
        }

        public void StartBuild(GameObject u, Timer t)
        {
            unit = u;
            timer = t;
            timer.TimeOutEvent += Spawn;
        }

        public float GetTime() => unit.GetComponent<UnitFacade>().GetBuildTime();

        private void Spawn()
        {
            UnitTransform tr = SetUnit.Create(
                point.transform.position,
                transform.rotation);
            unitFactory.Create(unit, tr);
        }
    }
}
