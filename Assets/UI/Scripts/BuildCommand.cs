using UnityEngine;

namespace RTS
{
    public interface IBUttonInit
    {
        public void Init(int id, BuildUnit b);
    }

    public class BuildCommand : MonoBehaviour, IBUttonInit //Buttons responsible for unit production.
    {
        private BuildUnit builder;
        private int ID;
        private GameObject unit;

        public void Init(int id, BuildUnit b)
        {
            builder = b;
            ID = id;
            unit = builder.GetUnit(ID);
            Recovery();

            builder.queue.BuildStartEvent += StartBuild;
            builder.queue.BuildAddEvent += ChangeCount;
            builder.queue.BuildEndEvent += Clear;
            builder.queue.BuildRemoveEvent += Clear;
        }

        public void Command()
        {
            if(builder.queue.currentUnit != unit) { builder.Add(ID); return; }

            builder.StartQueue(ID);
            GetComponent<ButtonProgressBar>()?.Set(1);
        }

        private void Recovery()
        {
            int count = builder.queue.QueueCount(unit);
            if (count == 0) return;

            if(unit == builder.queue.currentUnit) 
                GetComponent<ButtonProgressBar>()?.Recovery(builder?.queue?.timer);
            else
                GetComponent<ButtonProgressBar>()?.Set(1);

            GetComponent<ButtonCounter>()?.SetCount(count);
        }

        private void StartBuild(Timer t, GameObject u)
        {
            if (u != unit) return;
            GetComponent<ButtonProgressBar>()?.Run(t);
        }

        private void ChangeCount(GameObject u)
        {
            if(u != unit) return;

            GetComponent<ButtonCounter>().SetCount(builder.queue.QueueCount(u)); 
            GetComponent<ButtonProgressBar>().Set(1);
        }

        private void Clear(GameObject u)
        {
            int count = builder.queue.QueueCount(u);

            if (u == unit && count <= 0)
                GetComponent<ButtonProgressBar>()?.Set(0);

            if (u != unit && u != null) return;
                GetComponent<ButtonCounter>().SetCount(count);
        }

        //private void Clear(GameObject u)
        //{
        //    int count = builder.queue.QueueCount(u);
        //    if (u != unit && u != null) return;
        //        GetComponent<ButtonCounter>().SetCount(count);
        //
        //    if (u == unit && count <= 0) return;
        //        GetComponent<ButtonProgressBar>()?.Set(count > 0 ? 1 : 0);
        //}

        public void RightClick()
        {
            if (builder.queue.QueueCount(unit) == 0) return;

            if (builder.queue.currentUnit == unit) builder.Undo(ID); 
            else builder.Remove(ID);
        }

        public void OnDestroy()
        {
            builder.queue.BuildStartEvent -= StartBuild;
            builder.queue.BuildAddEvent -= ChangeCount;
            builder.queue.BuildEndEvent -= Clear;
            builder.queue.BuildRemoveEvent -= Clear;
        }
    }
}

