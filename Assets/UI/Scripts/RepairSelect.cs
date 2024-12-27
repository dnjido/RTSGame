namespace RTS
{
    public class RepairSelect : ActionSelect
    {
        public override void Press()
        {
            action.Set(new RepairAction(team));
            CursorIcon.Set(icon);
        }
    }
}
