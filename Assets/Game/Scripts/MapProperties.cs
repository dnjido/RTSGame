using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    [Serializable]
    public struct MapProperties
    {
        public string name;
        public string scene;
        public Texture2D thumbnail;
        public int playerCount;
        public Vector2 size;
    }
}
