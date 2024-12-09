using UnityEngine;
using Zenject;

namespace RTS
{
    public struct MoveStruct
    {
        public Vector3 point;
        public GameObject unit;
        public bool enemy;

        public void SetPoint(Vector3 p)
        {
            unit = null;
            enemy = false;
            point = p;
        }

        public void SetPoint(GameObject u)
        {
            unit = u;
            enemy = u.GetComponent<UnitTeam>().team != 1;

        }
    }

    public abstract class TemplateMovement : MonoBehaviour // Units Movement
    {
        [SerializeField] protected float speed;
        public float getSpeed => speed;

        public bool hasMove;
        protected MoveStruct moveStruct;
        protected IState updater;

        protected IMovement moveType;
        protected abstract void SetMoveType();

        protected virtual void Start()
        {
            moveStruct = new MoveStruct();
            moveStruct.point = transform.position;
        }

        [Inject]
        public void UnitStats(GetStats g)
        {
            speed = g.Stats(gameObject).moveStats.speed;
        }

        void FixedUpdate() => StateUpdater();

        private void StateUpdater()
        {
            if (updater != null) updater.Update();
        }

        public virtual void SetPoint(Vector3 p)
        {
            moveStruct.SetPoint(p);

            if (Input.GetKey("a"))
                updater = new MoveAttackState(gameObject, moveType, p);
            else
                updater = new MoveState(gameObject, moveType, p);
        }

        protected virtual void SetUnit(GameObject u)
        {
            moveStruct.SetPoint(u);

            if (!moveStruct.enemy)
                updater = new FollowState(gameObject, moveType, u);
            else
                updater = new AttackState(gameObject, moveType, u);
        }

        public void Command()
        {
            GameObject ray = CursorRay.RayUnit();

            SetMoveType();

            if (ray != null && ray.tag == "Unit")
                SetUnit(ray);
            else
                SetPoint(CursorRay.RayPoint());

        }
    }

    public class TargetMove
    {
        public static bool Move(Vector3 old, Vector3 _new, float tolerance) =>
            Vector3.Distance(old, _new) > tolerance;
    }
}
