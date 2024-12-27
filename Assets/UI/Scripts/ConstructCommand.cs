using Unity.VisualScripting;
using UnityEngine;

namespace RTS
{
    public class ConstructCommand : MonoBehaviour, IBUttonInit, IPlacedEvent //Buttons responsible for structure construction.
    {
        private ConstructBuild builder;
        private int ID;
        private GameObject unit;
        public Placing placedUnit;

        public void Init(int id, BuildUnit b)
        {
            builder = b as ConstructBuild;
            ID = id;
            unit = builder.GetUnit(ID);
            Recovery();

            OnEnable();
        }

        public void Command()
        {
            if (builder.queue.LastUnit(unit) != null) { Resume(); return; }

            if (!builder.complete)
            {
                builder.StartQueue(ID);
                GetComponent<ButtonProgressBar>()?.Set(1);
            }
            else builder.PlacingCommand(this);
        }

        public void Placing()
        {
            Clear(null);
            builder.complete = null;
        }

        public void Resume() 
        {
            if (builder.timer != null && builder.timer.pause == true) 
                builder.timer.pause = false;
        }

        private void Recovery()
        {
            if (builder.complete == unit)
                GetComponent<ButtonProgressBar>()?.Set(1);

            if (builder.queue.LastUnit(unit) == null) return;

            if (builder.queue.currentUnit == unit)
                GetComponent<ButtonProgressBar>()?.Recovery(builder?.queue?.timer);
        }

        private void StartBuild(Timer t, GameObject u)
        {
            if (u != unit) return;
            GetComponent<ButtonProgressBar>()?.Run(t);
        }

        private void Complete(GameObject u)
        {
            if (u != unit) return;
            GetComponent<ButtonProgressBar>()?.Set(1);
        }

        private void Clear(GameObject u)
        {
            builder.complete = null;
            GetComponent<ButtonProgressBar>()?.Set(0);
        }

        public void RightClick()
        {
            if (builder.queue.currentUnit != unit) return;

            builder.Undo(ID);
        }

        public void OnEnable()
        {
            if (!builder) return;
            builder.queue.BuildStartEvent += StartBuild;
            builder.queue.BuildEndEvent += Complete;
            builder.queue.BuildRemoveEvent += Clear;
        }

        public void OnDisable()
        {
            if (!builder) return;
            builder.queue.BuildStartEvent -= StartBuild;
            builder.queue.BuildEndEvent -= Complete;
            builder.queue.BuildRemoveEvent -= Clear;
        }
    }
}

