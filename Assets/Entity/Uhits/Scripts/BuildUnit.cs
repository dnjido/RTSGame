using UnityEngine;
using System.Collections;
using Zenject;

namespace RTS
{
    public class BuildUnit : MonoBehaviour  // Produces units that are in the queue.
    {
        [field: SerializeField] public GameObject ButtonPrefab { get; private set; }
        [field: SerializeField] public Transform Parent { get; private set; }
        [SerializeField] private Transform point, movePoint;
        [SerializeField] private GameObject[] unitsList;

        private UnitFacade.Factory unitFactory;
        public BuildQueue queue { get; private set; }
        ButtonSpawner buttons;

        public int countList => unitsList.Length;

        private void Update()
        {
            queue?.timer?.Tick();
            if (GetComponent<Selection>().isSelected) SetMovePoint();
        }

        [Inject]
        public void UnitFactory(UnitFacade.Factory f) =>
            unitFactory = f;

        [Inject]
        public void UnitButtonFactory(ButtonFacade.Factory f) =>
            buttons = new ButtonSpawner(f, this);

        private void Start()
        {
            queue = new BuildQueue();
            queue.BuildSpawnEvent += Spawn;
            GetComponent<Selection>().SelectedEvent += Selected;
        }

        private void Selected(bool s)
        {
            movePoint.GetChild(0).gameObject.SetActive(s);
            buttons.Buttons(s);
        }

        public void StartQueue(int id) =>
            SelectClass(id)?.Start();

        public void Undo(int id) =>
            SelectClass(id)?.Undo();

        public GameObject GetUnit(int id) => unitsList[id];

        public float BuildTime(int id) =>
            unitsList[id].GetComponent<UnitFacade>().GetBuildTime();

        private BuilderStates SelectClass(int id)
        {
            BuildCommandsStruct str = MakeBuildStruct.Make(unitsList[id], BuildTime(id));

            if (queue?.timer == null)
                return new BuilderStateIdle(queue, str);
            if (queue?.timer?.pause == false)
                return new BuilderStateProcess(queue, str);
            else
                return new BuilderStatePause(queue, str);
        }

        private void SetMovePoint()
        {
            if (Input.GetMouseButtonDown(1))
                movePoint.transform.position = CursorRay.RayPoint();
        }

        private void Spawn(GameObject unit) => StartCoroutine(SpawnCoroutine(unit));

        IEnumerator SpawnCoroutine(GameObject unit)
        {
            UnitTransform tr = SetUnit.Create(
                point.position,
                transform.rotation,
                GetComponent<UnitTeam>().team);
            GameObject u = unitFactory.Create(unit, tr);
            yield return new WaitForSeconds(Time.deltaTime);
            u.GetComponentInChildren<UnitMovement>().SetPoint(movePoint.position);
        }
    }
}