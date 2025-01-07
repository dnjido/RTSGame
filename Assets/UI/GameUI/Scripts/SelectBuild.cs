using System;
using System.Linq;
using UnityEngine;
using Zenject;
using System.Collections.Generic;

namespace RTS
{
    public class SelectBuild : MonoBehaviour // Select build type from category
    {
        [SerializeField] private GameObject prefabs;
        [SerializeField] private GameObject[] buttons;
        [SerializeField] private bool alt;
        private bool created;

        [SerializeField] private int id;
        [SerializeField] private new string tag;

        //[SerializeField] private int team = 1;

        private Func<GameObject, SelectBuild> selBuild = obj => obj.GetComponent<SelectBuild>();

        private List<GameObject> BuildList() => 
            fabricList[0].List(tag);

        private FabricsList[] fabricList;

        [Inject]
        public void FabricsList(FabricsList[] f) =>
            fabricList = f;

        public void GetBuild()
        {
            try
            {
                ClearAllBuild();
                GameObject b = BuildList()[id];

                b.GetComponent<Selection>().isSelected = true;
                AltBuild(b);
                created = true;
            }
            catch { }
        }

        public void ClearBuild()
        {
            BuildList()[id].GetComponent<Selection>().isSelected = false;
            created = false;
        }

        public void ClearAllBuild()
        {
            GameObject match;
            try
            {
                match = buttons.Single(p => selBuild(p).created == true); 
            }
            catch (InvalidOperationException) { return; }

            selBuild(match).ClearBuild();

            if (selBuild(match) == this) 
                id = id++ >= BuildList().Count - 1 ? 0 : id++;
            else
                selBuild(match).id = 0;
        }

        public void AltBuild(GameObject build)
        {
            if (build.GetComponent<ConstructFortification>())
            {
                if (alt) build.GetComponent<ConstructFortification>().ClearButtons();
                else build.GetComponent<ConstructBuild>().ClearButtons();
            }
        }
    }
}
