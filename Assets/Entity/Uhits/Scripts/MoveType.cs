using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace RTS
{
    public interface IMovement
    {
        public void StopMove();
        public void StartMove(Vector3 point);
    }

    public class NavMeshMove : IMovement // Moving with NavMesh
    {
        readonly NavMeshAgent agent;
        readonly GameObject unit;

        public NavMeshMove(GameObject u)
        {
            unit = u;
            agent = unit.GetComponent<NavMeshAgent>();
        }

        public void StopMove() =>
            agent.SetDestination(unit.transform.position);

        public void StartMove(Vector3 point) =>
            agent.SetDestination(point);
    }

    public class TweenMove : IMovement // Moving with DOTween
    {
        readonly Sequence mySequence = DOTween.Sequence();
        readonly float speed;
        readonly GameObject unit;

        public TweenMove(GameObject u)
        {
            unit = u;
            speed = unit.GetComponent<TemplateMovement>().getSpeed;
        }

        public void StopMove()
        {
            unit.transform.DOMove(unit.transform.position, 0);
        }

        public void StartMove(Vector3 point)
        {
            point.y = unit.transform.position.y;
            unit.transform.DOMove(point, speed).SetSpeedBased(true);
            unit.transform.DOLookAt(point, 0.5f, AxisConstraint.Y);
        }
    }
}
