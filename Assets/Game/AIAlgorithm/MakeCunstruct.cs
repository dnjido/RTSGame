using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RTS
{
    public class MakeConstruct // AI build construct
    {
        readonly private AIManadger manadger;

        private FabricsList builders => manadger.builders;
        private PlayerResources resources => manadger.resources;
        private GameObject[] buildList => manadger.getBuildList;
        private int buildID;

        public MakeConstruct(AIManadger m) 
        {
            manadger = m;
            manadger.AIActionEvent += BuildCommand;
        }

        private void BuildCommand()
        {
            GetAllBuilders();
        }

        private GameObject SearchBuild()
        {
            try
            {
                GameObject obj = buildList[buildID];
                buildID++;
                return obj;
            }
            catch { return null; }
        }

        public void GetAllBuilders()
        {
            BuildersList(builders.Yards);
        }

        private void StartQueue(GameObject builder)
        {
            BuildUnit build = builder.GetComponent<ConstructBuild>();

            if (build.QueueCount() <= 0 && resources.money >= 10)
                try { build.StartQueue(build.GetID(SearchBuild())); }
                catch { }
        }

        public void BuildersList(List<GameObject> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                StartQueue(list[i]);
                CheckQueue(list[i]);
            }
        }

        public void CheckQueue(GameObject builder)
        {
            ConstructBuild build = builder.GetComponent<ConstructBuild>();

            if (!build.complete) return;

            SearchFreeSpace(build.SpawnConstruct().GetComponent<Placing>(), builder);
        }

        public void SearchFreeSpace(Placing construct, GameObject builder)
        {            
            Vector3 center = builder.transform.position;
            Vector3 size = construct.size * 1.5f;
            List<Vector3> list = new List<Vector3>();

            for (int x = -2; x < 3; x++)
                for (int z = -2; z < 3; z++)
                {
                    Vector3 coord = new Vector3(size.x * x + center.x, center.y, size.z * z + center.z);
                    if (construct.IsAreaClear(coord)) list.Add(coord);
                }

            construct.placedEvent = builder.GetComponent<ConstructBuild>();
            if (list.Count <= 0) construct.RemoveAI();
            else construct.PlaceAI(list[Random.Range(0, list.Count - 1)]);
        }
    }
}
