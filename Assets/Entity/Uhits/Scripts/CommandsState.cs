using System.Drawing;
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
        public readonly GameObject unit;
        public readonly Vector3 point;
        public readonly IMovement move;

        public MoveState(GameObject u, IMovement m, Vector3 p) // Move command
        {
            unit = u;
            point = p;
            move = m;
            Start();
        }

        public void Start() => 
            move.StartMove(point);

        public void Update() { }

        public void End() =>
            move.StopMove();
    }

    public class FollowState : IState // Follow command
    {
        public readonly GameObject unit;
        public readonly GameObject unitMove;
        public readonly IMovement move;
        private Vector3 oldPos;

        public FollowState(GameObject u, IMovement m, GameObject um)
        {
            unit = u;
            unitMove = um;
            move = m;
            Start();
        }

        public void Start() =>
            move.StartMove(unitMove.transform.position);

        public void Update()
        {
            if (!unitMove) return;

            if (!TargetMove.Move(oldPos, unitMove.transform.position, 0.01f)) return;

            oldPos = unitMove.transform.position;
            Start();
        }

        public void End() =>
            move.StopMove();
    }

    public class AttackState : IState // Attack command
    {
        public readonly GameObject unit;
        public readonly GameObject unitMove;
        public readonly IMovement move;
        public readonly DetectEnemy detector;
        private Vector3 oldPos;
        private bool HasTarget;

        public AttackState(GameObject u, IMovement m, GameObject um)
        {
            unit = u;
            unitMove = um;
            move = m;
            detector = GU.NullComponent<DetectEnemy>(unit);
            Start();
        }

        public void Start() =>
            move.StartMove(unitMove.transform.position);

        public void Update()
        {
            if (!unitMove) return;

            if (TargetMove.Move(oldPos, unitMove.transform.position, 0.01f))
            {
                oldPos = unitMove.transform.position;
                move.StartMove(unitMove.transform.position);
            }

            if (Vector3.Distance(unit.transform.position, unitMove.transform.position) <= detector.GetRange())
            {
                if (!HasTarget) SetTarget();
            }
            else HasTarget = false;
        }

        private void SetTarget()
        {
            move.StopMove();
            HasTarget = true;
            detector.SetTarget(unit);
        }

        public void End() => 
            move.StopMove();
    }

    public class MoveAttackState : IState // Attack while moving command
    {
        public readonly GameObject unit;
        public readonly Vector3 point;
        public readonly IMovement move;
        public readonly DetectEnemy detector;
        private bool hasTarget;

        public MoveAttackState(GameObject u, IMovement m, Vector3 p)
        {
            unit = u;
            point = p;
            move = m;
            detector = GU.NullComponent<DetectEnemy>(unit);
            Start();
        }

        public void Start() =>
            move.StartMove(point);

        public void Update()
        {
            if (detector.GetUnit() != null && !hasTarget)
                HasTarget();

            if (detector.GetUnit() == null && hasTarget)
            {
                hasTarget = false;
                move.StartMove(point);
            }
        }

        private void HasTarget()
        {
            hasTarget = true;
            move.StopMove();
        }

        public void End()
        {
            move.StopMove();
        }
    }

    public class MineState : IState
    {
        public readonly GameObject unit;
        public readonly GameObject unitMove;
        public readonly IMovement move;
        protected readonly GameObject returnPoint;
        protected readonly Vector3 point;
        protected readonly HarvestMovement moveH;
        protected readonly NavMeshAgent agent;
        protected readonly OreMining mine;

        protected bool busy;

        public MineState(GameObject u, IMovement m, GameObject um, GameObject r) // Move command
        {
            unit = u;
            unitMove = um;
            move = m;
            point = um.GetComponent<IHarvest>().point;
            returnPoint = r;
            mine = unit.GetComponent<OreMining>();
            moveH = unit.GetComponent<HarvestMovement>();
            agent = unit.GetComponent<NavMeshAgent>();
            Start();
        }

        public virtual void Start()
        {
            move.StartMove(point);
            unit.GetComponent<NavMeshAgent>().stoppingDistance = 0; 
        }

        public virtual void Update()
        {
            GetDistance();

            if (!unitMove || unitMove.GetComponent<Mine>().empty)
                moveH.RefreshMine();

            if (mine.full) End();
        }

        public virtual void Deploy() =>
            mine.Load(unitMove);

        public void GetDistance()
        {
            if (DeployDist(1)) Deploy();

            if (unitMove)
                SetBusy(unitMove.GetComponent<IHarvest>().currentHarvester && DeployDist(5));
        }

        public void SetBusy(bool b)
        {
            if (b == busy) return;


            agent.stoppingDistance = !b ? 0 : 5;
            agent.SetDestination(!b ? point : unit.transform.position);
            busy = b;
        }

        public void End()
        {
            if (!returnPoint) return;

            unitMove.GetComponent<IHarvest>().ClearHarvester();
            moveH.MoveTo(returnPoint);
        }

        protected bool DeployDist(float d) => 
            Vector3.Distance(unit.transform.position, point) <= d;
    }

    public class PlantState : MineState, IState
    {
        public PlantState(GameObject u, IMovement m, GameObject um, GameObject r) : base(u, m, um, r) { }

        public override void Deploy() =>
            mine.Unload(unitMove);

        public override void Update()
        {
            GetDistance();

            if (unitMove == null || unitMove.GetComponent<UnitTeam>().team != unit.GetComponent<UnitTeam>().team)
                unit.GetComponent<HarvestMovement>().RefreshPlant();

            if (mine.empty) End(); 
        }
    }
}

