using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public interface IBUttonInit
    {
        public void Init(int id, BuildUnit b, GetStats stats);
    }

    public class BuildCommand : ButtonCommandTemplare, IBUttonInit //Buttons responsible for unit production.
    {
        protected override void SetBuilder(int id, BuildUnit b)
        {
            builder = b;
            ID = id;
            unit = builder.GetUnit(ID);
            if (builder?.queue != null) queue = builder.queue;
        }

        public override void Command()
        {
            if(queue.currentUnit != unit) { builder.Add(ID); return; }

            builder.StartQueue(ID);
            progressBar?.Set(1);
        }

        protected override void Recovery()
        {
            int count = queue.QueueCount(unit);
            if (count == 0) return;

            if(unit == queue.currentUnit) 
                progressBar?.Recovery(builder?.queue?.timer);
            else
                progressBar?.Set(1);

            counter?.SetCount(count);
        }

        protected override void StartBuild(Timer t, GameObject u)
        {
            if (!isCurrentButton(u)) return;
            progressBar?.Run(t);
        }

        protected override void ChangeCount(GameObject u)
        {
            if(!isCurrentButton(u)) return;

            counter.SetCount(queue.QueueCount(u)); 
            progressBar.Set(1);
        }

        protected override void Clear(GameObject u)
        {
            int count = queue.QueueCount(u);

            if (isCurrentButton(u) && count <= 0)
                progressBar?.Set(0);

            if (!isCurrentButton(u) && u != null) return;
                counter.SetCount(count);
        }

        public override void RightClick()
        {
            if (queue.QueueCount(unit) == 0) return;

            if (queue.currentUnit == unit) builder.Undo(ID); 
            else builder.Remove(ID);

            if (queue.QueueCount(unit) == 0) 
                progressBar?.Set(0);
        }

        protected override void Enable()
        {
            if (builder == null) return;
            queue.BuildStartEvent += StartBuild;
            queue.BuildAddEvent += ChangeCount;
            queue.BuildEndEvent += Clear;
            queue.BuildRemoveEvent += Clear;
        }

        protected override void Disable()
        {
            if (builder == null) return;
            queue.BuildStartEvent -= StartBuild;
            queue.BuildAddEvent -= ChangeCount;
            queue.BuildEndEvent -= Clear;
            queue.BuildRemoveEvent -= Clear;
        }
    }
}

