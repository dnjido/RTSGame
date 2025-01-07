using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace RTS
{
    public class SetMapBar : MonoBehaviour, IUICreate<int>
    {
        [SerializeField] new TMP_Text name;
        [SerializeField] int ID;
        public MapProperties[] properties;

        private StartGameProperties startGameProperties;

        [Inject]
        public void Init(StartGameProperties sg) =>
            startGameProperties = sg;

        [Inject]
        public void SetProperties(MapProperties[] maps) =>
            properties = maps;


        public void Create(int i)
        {
            ID = i;
            name.text = properties[ID].name;
        }

        public void Select() =>
            startGameProperties.SetMap(properties[ID]);
    }
}
