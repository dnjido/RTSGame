using System;
using UnityEngine;
using Zenject;

namespace RTS
{
    public struct UnitButtonStruct
    {
        public int id;
        public BuildUnit builder;
        public Transform parent;
    }

    public class MakeUnitButtonStruct
    {
        static public UnitButtonStruct Make(int id, BuildUnit b, Transform p)
        {
            UnitButtonStruct str = new UnitButtonStruct();
            str.id = id;
            str.builder = b;
            str.parent = p;
            return str;
        }
    }


    public class ButtonFacade : MonoBehaviour, IDisposable
    {
        public void Dispose()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
            Destroy(gameObject);
        }


        public class Factory : PlaceholderFactory<GameObject, UnitButtonStruct, GameObject>
        {
        }
    }
}
