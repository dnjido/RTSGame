using UnityEngine;
using Zenject;

namespace RTS
{
    public class BuildUnit : MonoBehaviour
    {
        [SerializeField] private GameObject point, buttonPrefab;
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject[] unitsList;
        UnitFacade.Factory unitFactory;
        //private BuilderStates builderStates;
        private BuildQueue queue;

        private void Update() => queue?.timer?.Tick();

        private void Start() 
        {
            queue = new BuildQueue();
            queue.BuildSpawnEvent += Spawn;
            AddButtons();
        }

        private void AddButtons()
        {
            for (int i = 0; i < unitsList.Length; i++)
            {
                GameObject b = Instantiate(buttonPrefab, parent);
                b.GetComponent<BuildCommand>().SetBuilder(i, this);
            }
        }

        [Inject]
        public void ProjectileFactory(UnitFacade.Factory f) =>
            unitFactory = f;

        public void StartQueue(int id) => 
            SelectClass(id)?.Start();

        public void Undo(int id) =>
            SelectClass(id)?.Undo();

        public BuildQueue GetQueue()  => queue;

        public GameObject GetUnit(int id)  => unitsList[id];

        public int GetQueueCount(GameObject u)  => queue.QueueCount(u);

        public float BuildTime(int id) => 
            unitsList[id].GetComponent<UnitFacade>().GetBuildTime();

        private BuilderStates SelectClass(int id)
        {
            BuildCommandsStruct str = MakeBuildStruct.Make(unitsList[id], BuildTime(id));

            if (queue.timer == null)
                return new BuilderStateIdle(queue, str);
            if (queue.timer.GetPause() == false)
                return new BuilderStateProcess(queue, str);
            else
                return new BuilderStatePause(queue, str);
        }

        private void Spawn(GameObject unit)
        {
            UnitTransform tr = SetUnit.Create(
                point.transform.position,
                transform.rotation);
            unitFactory.Create(unit, tr);
        }
    }
}
