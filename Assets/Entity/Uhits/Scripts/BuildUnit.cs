using UnityEngine;
using System.Collections;
using Zenject;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System;

namespace RTS
{
    public class BuildUnit : MonoBehaviour  // Produces units that are in the queue.
    {
        [field: SerializeField] public GameObject ButtonPrefab { get; protected set; }
        public Transform panel { get; private set; }

        [SerializeField] private Transform point, movePoint;
        [SerializeField] protected GameObject[] unitsList;

        private UnitFacade.Factory unitFactory;
        public BuildQueue queue { get; private set; }
        public PlayerResources[] money { get; private set; }
        public GetStats stats { get; private set; }
        public Timer timer => queue.timer;
        protected ButtonSpawner buttons;

        public int countList => unitsList.Length;
        public int team => GetComponent<UnitTeam>().team;

        private void Update()
        {
            if (GetComponent<Selection>().isSelected) SetMovePoint();
            if (timer == null) return;

            ChangeResource();
        }

        [Inject]
        public void UnitStats(GetStats g)
        {
            stats = g;
            SetUnitList();
        }

        public virtual void SetUnitList()
        {
            unitsList = stats.Stats(gameObject).buildStats.units;
            ButtonPrefab = stats.Stats(gameObject).buildStats.buttons;
        }

        [Inject]
        public void Resource(PlayerResources[] r) =>
            money = r;

        [Inject]
        public void ButtonPanel(SidePanel p) =>
            panel = p.transform;

        [Inject]
        public void UnitFactory(UnitFacade.Factory f) =>
            unitFactory = f;

        [Inject]
        public void ButtonFactory(ButtonFacade.Factory f) =>
            buttons = new ButtonSpawner(f, this);

        private void Awake()
        {
            queue = new BuildQueue();
        }

        protected void Selected(bool s)
        {
            movePoint.GetChild(0).gameObject.SetActive(s);
            MakeButtons(s);
        }

        private void Destroy()
        {
            buttons.Buttons(false);
        }

        public void MakeButtons(bool s) =>
            buttons.Buttons(s);

        private void ChangeResource()
        {
            int team = GU.Team(gameObject);
            float tick = stats.Stats(queue.currentUnit).generalStats.costPerTick;
            if (money[team - 1].CheckAndChange(-tick))
                queue?.timer?.Tick();
        }

        public void StartQueue(int id) =>
            SelectClass(id)?.Start();

        public void Undo(int id) =>
            SelectClass(id)?.Undo();

        public void Remove(int id)
        {
            BuildCommandsStruct str = MakeBuildStruct.Make(unitsList[id], BuildTime(id));
            queue?.Remove(str);
        }

        public void Add(int id) =>
            queue?.BuildAdd(unitsList[id], BuildTime(id));

        public GameObject GetUnit(int id) => unitsList[id];

        public int GetID(GameObject id)
        {
            return unitsList.Select((unit, index) => new { unit, index })
                    .FirstOrDefault(x => x.unit == id)?.index ?? -1;
        }

        

        public float BuildTime(int id) =>
            stats.Stats(unitsList[id]).generalStats.buildTime;

        public float BuildCost(int id) =>
            stats.Stats(unitsList[id]).generalStats.cost;

        public int QueueCount() =>
            queue.buildCount;

        public List<int> GetWithAttr(string attr) 
        {
            List<int> list = unitsList
                .Select((unit, index) => new { unit, index })
                .Where(x => GetAttr.GetAttribute(x.unit, attr)) 
                .Select(x => x.index).ToList();

            return list;
        }

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

        protected virtual void Complete(GameObject unit)
        {
            GameObject u = Spawn(unit);
            StartCoroutine(MoveToPoint(u));
        }

        protected GameObject Spawn(GameObject unit)
        {
            UnitTransform tr = SetUnit.Create(
                point.position,
                transform.rotation,
                team);
            return unitFactory.Create(unit, tr);
        }

        private IEnumerator MoveToPoint(GameObject unit)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            unit.GetComponent<TemplateMovement>().SetPoint(movePoint.position);
        }

        private void OnEnable()
        {
            queue.BuildSpawnEvent += Complete;
            GetComponent<Selection>().SelectedEvent += Selected;
            GetComponent<HealthSystem>().DeathEvent += Destroy;
        }

        private void OnDisable()
        {
            queue.BuildSpawnEvent -= Complete;
            GetComponent<Selection>().SelectedEvent -= Selected;
            GetComponent<HealthSystem>().DeathEvent -= Destroy;
        }
    }
}