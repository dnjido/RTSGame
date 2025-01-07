using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RTS
{
    public class PlayersPanel : MonoBehaviour
    {
        [SerializeField] private int count;
        [SerializeField] private GameObject panel;

        private GenericUIFactory factory;

        [Inject]
        public void Factory(GenericUIFactory f)
        {
            factory = f;

            for (int i = 0; i < count; i++) 
                Spawn(i);
        }

        public void Spawn(int id)
        {
            GameObject obj = factory.Create(panel, id);
            obj.transform.SetParent(transform, false);
        }
    }
}
