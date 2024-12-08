using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public interface IState { 
        void Start();
        void Update();
        void End();
    }

    public class MoveState : IState
    {
        readonly NavMeshAgent agent;
        readonly Vector3 point;

        public MoveState(NavMeshAgent a, Vector3 p) // Move command
        {
            agent = a;
            point = p;
            Start();
        }

        public void Start()
        {
            agent.SetDestination(point);
        }

        public void Update() { }

        public void End()
        {
            agent.SetDestination(agent.transform.position);
        }
    }

    public class FollowState : IState // Follow command
    {
        public readonly NavMeshAgent agent;
        public readonly GameObject unit;
        private Vector3 oldPos;

        public FollowState(NavMeshAgent a, GameObject u)
        {
            agent = a;
            unit = u;
            Start();
        }

        public void Start() =>
            agent.SetDestination(unit.transform.position);

        public void Update()
        {
            if (!unit) return;

            if (!TargetMove.Move(oldPos, unit.transform.position, 0.01f)) return;

            oldPos = unit.transform.position;
            Start();
        }

        public void End()
        {
            agent.SetDestination(agent.transform.position);
        }
    }

    public class AttackState : IState // Attack command
    {
        public readonly NavMeshAgent agent;
        public readonly GameObject unit;
        public readonly DetectEnemy detector;
        private Vector3 oldPos;
        private bool HasTarget;

        public AttackState(NavMeshAgent a, GameObject u)
        {
            agent = a;
            unit = u;
            detector = GetUnitStats.Detector(agent.gameObject);
            Start();
        }

        public void Start() =>
            agent.SetDestination(unit.transform.position);

        public void Update()
        {
            if (!unit) return;

            if (TargetMove.Move(oldPos, unit.transform.position, 0.01f))
            {
                oldPos = unit.transform.position;
                Start();
            }

            if (Vector3.Distance(agent.gameObject.transform.position, unit.transform.position) <= detector.GetRange())
            {
                if (!HasTarget) SetTarget();
            }
            else HasTarget = false;
        }

        private void SetTarget()
        {
            agent.SetDestination(agent.gameObject.transform.position);
            HasTarget = true;
            detector.SetTarget(unit);
        }

        public void End()
        {
            agent.SetDestination(agent.transform.position);
        }
    }

    public class MoveAttackState : IState // Attack while moving command
    {
        public readonly NavMeshAgent agent;
        public readonly Vector3 point;
        public readonly DetectEnemy detector;
        private bool hasTarget;

        public MoveAttackState(NavMeshAgent a, Vector3 p, DetectEnemy d)
        {
            agent = a;
            point = p;
            detector = d;
            Start();
        }

        public void Start() =>
            agent.SetDestination(point);

        public void Update()
        {
            if (detector.GetUnit() != null && !hasTarget)
                HasTarget();

            if (detector.GetUnit() == null && hasTarget)
            {
                hasTarget = false;
                Start();
            }
        }

        private void HasTarget()
        {
            hasTarget = true;
            agent.SetDestination(agent.gameObject.transform.position);
        }

        public void End()
        {
            agent.SetDestination(agent.transform.position);
        }
    }

    public class MineState : IState
    {
        private readonly NavMeshAgent agent;
        protected readonly GameObject harvester;
        protected readonly GameObject target;
        protected readonly GameObject returnPoint;
        protected readonly Vector3 point;
        protected readonly HarvestMovement move;
        protected readonly OreMining mine;

        protected bool busy;

        public MineState(NavMeshAgent a, GameObject t, GameObject h, GameObject r) // Move command
        {
            agent = a;
            target = t;
            point = t.GetComponent<IHarvest>().point;
            harvester = h;
            returnPoint = r;
            mine = harvester.GetComponent<OreMining>();
            move = harvester.GetComponent<HarvestMovement>();
            Start();
        }

        public virtual void Start()
        {
            agent.SetDestination(point);
            agent.stoppingDistance = 0; 
        }

        public virtual void Update()
        {
            GetDistance();

            if (!target || target.GetComponent<Mine>().empty)
                move.RefreshMine();

            if (mine.full) End();
        }

        public virtual void Deploy() =>
            mine.Load(target);

        public void GetDistance()
        {
            if (DeployDist(1)) Deploy();

            if (target)
                SetBusy(target.GetComponent<IHarvest>().currentHarvester && DeployDist(5));
        }

        public void SetBusy(bool b)
        {
            if (b == busy) return;


            agent.stoppingDistance = !b ? 0 : 5;
            agent.SetDestination(!b ? point : agent.transform.position);
            busy = b;
        }

        public void End()
        {
            if (!returnPoint) return;

            target.GetComponent<IHarvest>().ClearHarvester();
            move.MoveTo(returnPoint);
        }

        protected bool DeployDist(float d) => 
            Vector3.Distance(harvester.transform.position, point) <= d;
    }

    public class PlantState : MineState, IState
    {
        public PlantState(NavMeshAgent a, GameObject t, GameObject h, GameObject r) : base(a, t, h, r) { }

        public override void Deploy() =>
            mine.Unload(target);

        public override void Update()
        {
            GetDistance();

            if (target == null || target.GetComponent<UnitTeam>().team != harvester.GetComponent<UnitTeam>().team)
                harvester.GetComponent<HarvestMovement>().RefreshPlant();

            if (mine.empty) End(); 
        }
    }
}

