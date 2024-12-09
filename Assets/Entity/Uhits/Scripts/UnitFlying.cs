
namespace RTS
{

    public class UnitFlying : TemplateMovement // Moving flying units
    {
        protected override void SetMoveType() =>
            moveType = new TweenMove(gameObject);
    }
}