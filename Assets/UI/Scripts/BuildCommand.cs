using UnityEngine;

namespace RTS
{
    public class BuildCommand : MonoBehaviour
    {
        private BuildUnit builder;
        private int ID;

        public void SetBuilder(int id, BuildUnit b)
        {
            builder = b;
            ID = id;

            builder.GetQueue().BuildStartEvent += StartBuild;
            builder.GetQueue().BuildAddEvent += ChangeCount;
            builder.GetQueue().BuildEndEvent += Clear;
            builder.GetQueue().BuildRemoveEvent += Clear;
        }

        public void Command()
        {
            builder.StartQueue(ID);
            GetComponent<ButtonProgressBar>()?.Set(1);
        }

        private void StartBuild(Timer t, GameObject unit)
        {
            if (unit != builder.GetUnit(ID)) return;
            GetComponent<ButtonProgressBar>()?.Run(t);
        }

        private void ChangeCount(GameObject unit)
        {
            if(unit != builder.GetUnit(ID)) return;

            GetComponent<ButtonCounter>().SetCount(builder.GetQueueCount(unit)); 
            GetComponent<ButtonProgressBar>().Set(1);
        }

        private void Clear(GameObject unit)
        {
            int count = builder.GetQueueCount(unit);
            if (unit != builder.GetUnit(ID) && unit != null) return;
                GetComponent<ButtonCounter>().SetCount(count);

            if (unit == builder.GetUnit(ID) && count <= 0) return;
                GetComponent<ButtonProgressBar>()?.Set(count > 0 ? 1 : 0);
        }

        public void RightClick()
        {
            builder.Undo(ID);
        } 
    }
}

