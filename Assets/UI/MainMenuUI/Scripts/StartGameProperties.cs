using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using Zenject;

namespace RTS
{
    [Serializable]
    public class StartGameProperties
    {
        public MapProperties map;
        public PlayerPropertiesStruct[] playerProperties = new PlayerPropertiesStruct[8];

        public delegate void ChangeMapDelegate(MapProperties map);
        public event ChangeMapDelegate ChangeMapEvent;

        public void SetMap (MapProperties m)
        {
            map = m;
            ChangeMapEvent?.Invoke(map);
        }

        public void ClearPlayer(int id, bool a) =>
            playerProperties[id].active = a;

        public void SetProperties(PlayerPropertiesStruct p, int id) => playerProperties[id] = p;
    }
}

