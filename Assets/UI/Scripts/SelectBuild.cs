using RTS;
using UnityEngine;
using Zenject;

namespace RTS
{
    public class SelectBuild : MonoBehaviour
    {
        [SerializeField] private GameObject prefabs;
        [SerializeField] private GameObject[] buttons;
        [SerializeField] private bool alt;
        private bool created;

        [SerializeField] private int id;
        [SerializeField] private new string tag;

        private FabricsList[] fabricList;

        [Inject]
        public void FabricsList(FabricsList[] f) =>
            fabricList = f;

        private GameObject FindBuild()
        {
            string name = prefabs.name;
            return GameObject.Find(name);
        }

        public void GetBuild()
        {
            ClearAllBuild();
            //GameObject b = FindBuild();
            GameObject b = fabricList[0].List(tag)[id];

            b.GetComponent<Selection>().isSelected = true;
            AltBuild(b);
            created = true;
        }

        public void ClearBuild()
        {
            //FindBuild().GetComponent<Selection>().isSelected = false;
            fabricList[0].List(tag)[id].GetComponent<Selection>().isSelected = false;
            created = false;
        }

        public void ClearAllBuild()
        {
            foreach (GameObject b in buttons)
            {
                SelectBuild sel = b.GetComponent<SelectBuild>();
                if (sel.created)
                    sel.ClearBuild();

                if (sel == this) 
                {
                    id++;
                    if (id >= fabricList[0].List(tag).Count) id = 0;
                }
                //else sel.ClearBuild();
                //else { id = 0; print("b34br"); }
            }
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
