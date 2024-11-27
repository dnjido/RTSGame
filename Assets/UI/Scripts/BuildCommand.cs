using System;
using UnityEngine;
using Zenject;

namespace RTS
{
    public class BuildCommand : MonoBehaviour //Buttons responsible for unit production.
    {
        private BuildUnit builder;
        private int ID;

        public void SetBuilder(int id, BuildUnit b)
        {
            builder = b;
            ID = id;
            Recovery();

            builder.queue.BuildStartEvent += StartBuild;
            builder.queue.BuildAddEvent += ChangeCount;
            builder.queue.BuildEndEvent += Clear;
            builder.queue.BuildRemoveEvent += Clear;
        }

        public void Command()
        {
            builder.StartQueue(ID);
            GetComponent<ButtonProgressBar>()?.Set(1);
        }

        private void Recovery()
        {
            GameObject unit = builder.GetUnit(ID);
            int count = builder.queue.QueueCount(unit);
            if (count == 0) return;

            if(unit == builder.queue.currentUnit) 
                GetComponent<ButtonProgressBar>()?.Recovery(builder?.queue?.timer);
            else
                GetComponent<ButtonProgressBar>()?.Set(1);

            GetComponent<ButtonCounter>()?.SetCount(count);
        }

        private void StartBuild(Timer t, GameObject unit)
        {
            if (unit != builder.GetUnit(ID)) return;
            GetComponent<ButtonProgressBar>()?.Run(t);
        }

        private void ChangeCount(GameObject unit)
        {
            if(unit != builder.GetUnit(ID)) return;

            GetComponent<ButtonCounter>().SetCount(builder.queue.QueueCount(unit)); 
            GetComponent<ButtonProgressBar>().Set(1);
        }

        private void Clear(GameObject unit)
        {
            int count = builder.queue.QueueCount(unit);
            if (unit != builder.GetUnit(ID) && unit != null) return;
                GetComponent<ButtonCounter>().SetCount(count);

            if (unit == builder.GetUnit(ID) && count <= 0) return;
                GetComponent<ButtonProgressBar>()?.Set(count > 0 ? 1 : 0);
        }

        public void RightClick()
        {
            builder.Undo(ID);
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

