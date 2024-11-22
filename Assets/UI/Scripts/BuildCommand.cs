using UnityEngine;

namespace RTS
{

    public class BuildCommand : MonoBehaviour
    {
        [SerializeField] private GameObject builder, unit;
        private Timer timer;

        public void Command()
        {
            float buildTime = unit.GetComponent<UnitFacade>().GetBuildTime();
            timer = new Timer(buildTime);
            builder.GetComponent<BuildUnit>()?.StartBuild(unit, timer);
            GetComponent<ButtonProgressBar>()?.Run(timer);
        }

        private void Update()
        {
            if (timer != null) timer.Tick();
        }
    }
}

