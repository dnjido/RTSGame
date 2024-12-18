
namespace RTS
{
    public class ConstructFortification : ConstructBuild  // Produces fortification that are in the queue.
    {
        public override void SetUnitList()
        {
            unitsList = stats.Stats(gameObject).buildStats.secondUnits;
            ButtonPrefab = stats.Stats(gameObject).buildStats.buttons;
        }

        protected override void SelectedAlt(bool alt)
        {
            Selected(alt);
        }
    }
}