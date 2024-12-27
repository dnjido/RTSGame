namespace RTS
{
    public class SellSelect : ActionSelect
    {
        public override void Press()
        {
            action.Set(new SellAction(team));
            CursorIcon.Set(icon);
        }
    }
}
