using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public class ConstructCommand : ButtonCommandTemplare, IBUttonInit, IPlacedEvent //Buttons responsible for structure construction.
    {
        protected new ConstructBuild builder;
        protected GameObject complete => builder.complete;
        public Placing placedUnit;

        protected new ButtonProgressBar progressBar => GetComponent<ButtonProgressBar>();

        protected override void SetBuilder(int id, BuildUnit b)
        {
            builder = b as ConstructBuild;
            ID = id;
            unit = builder.GetUnit(ID);
            if (builder?.queue != null) queue = builder.queue;
        }

        public override void Command()
        {
            if (queue.buildCount != 0) { Resume(); return; }

            if (!builder.complete)
            {
                builder.StartQueue(ID);
                progressBar?.Set(1);
            }
            else if (builder.complete == unit) builder.PlacingCommand(this);
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

        protected override void Recovery()
        {
            if (isCurrentButton(builder.complete))
                progressBar?.Set(1);

            if (queue.QueueCount(unit) == 0) return;

            if (isCurrentButton(queue.currentUnit))
                progressBar?.Recovery(builder?.queue?.timer);
        }

        protected override void StartBuild(Timer t, GameObject u)
        {
            if (!isCurrentButton(u)) return;
            progressBar?.Run(t);
        }

        private void Complete(GameObject u)
        {
            if (!isCurrentButton(u)) return;
            progressBar?.Set(1);
        }

        protected override void Clear(GameObject u)
        {
            builder.complete = null;
            progressBar?.Set(0);
        }

        public override void RightClick()
        {
            if (isCurrentButton(queue.currentUnit)) builder.Undo(ID);

            if (builder.complete == unit) { builder.ClearComplete(); Clear(unit); }
        }

        protected override void ChangeCount(GameObject u){ }

        protected override void Enable()
        {
            if (builder == null) return;
            queue.BuildStartEvent += StartBuild;
            queue.BuildEndEvent += Complete;
            queue.BuildRemoveEvent += Clear;
        }

        protected override void Disable()
        {
            if (builder == null) return;
            queue.BuildStartEvent -= StartBuild;
            queue.BuildEndEvent -= Complete;
            queue.BuildRemoveEvent -= Clear;
        }
    }
}

