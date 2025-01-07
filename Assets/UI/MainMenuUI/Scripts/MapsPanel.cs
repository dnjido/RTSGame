using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RTS
{
    public class MapsPanel : MonoBehaviour
    {
        [SerializeField] private List<string> scenes = new List<string>();
        [SerializeField] private GameObject panel;

        private GenericUIFactory factory;

        [Inject]
        public void Factory(GenericUIFactory f)
        {
            factory = f;

            for (int i = 0; i < scenes.Count; i++)
                Spawn(i);
        }

        private void Start()
        {
            transform.GetChild(0).GetComponent<SetMapBar>().Select();
        }

        public void Spawn(int id)
        {
            GameObject obj = factory.Create(panel, id);
            obj.transform.SetParent(transform, false);
        }
            
    }
}
