using UnityEngine.AI;

namespace RTS
{

    public class UnitMovement : TemplateMovement // Moving a unit to a point or to another unit.
    {
        protected override void Start()
        {
            base.Start();
            GetComponent<NavMeshAgent>().speed = speed;
        }

        protected override void SetMoveType() =>
            moveType = new NavMeshMove(gameObject);
    }
}