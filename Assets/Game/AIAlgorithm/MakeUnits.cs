using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class MakeUnits  // AI make units
    {
        readonly private AIManadger manadger;

        private FabricsList builders => manadger.builders;
        private PlayerResources resources => manadger.resources;

        public MakeUnits(AIManadger m)
        {
            manadger = m;
            manadger.AIActionEvent += BuildCommand;
        }

        private void BuildCommand()
        {
            GetAllBuilders();
        }

        public void GetAllBuilders()
        {
            BuildersList(builders.Barracks);
            BuildersList(builders.Fabrics);
            BuildersList(builders.Aerodroms);
        }

        private void StartQueue(GameObject builder)
        {
            BuildUnit build = builder.GetComponent<BuildUnit>();

            if (build.QueueCount() <= 0 && resources.money >= 10)
                build.StartQueue(RandomCombatUnit(build));
        }

        private int RandomCombatUnit(BuildUnit builder)
        {
            var list = builder.GetWithAttr("Combat");

            int count = list.Count;
            return list[Random.Range(0, count)];
        }

        public void BuildersList(List<GameObject> list)
        {
            for (int i = 0; i < list.Count; i++)
                StartQueue(list[i]);
        }
    }
}
