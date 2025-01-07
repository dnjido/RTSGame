using UnityEngine;
using System.Collections.Generic;
using System;
using Zenject;

namespace RTS
{
    [Serializable]
    public class FabricsList // List of production build
    {
        public List<GameObject> Yards;
        public List<GameObject> Barracks;
        public List<GameObject> Fabrics;
        public List<GameObject> Aerodroms;
        public List<GameObject> Generators;
        public List<GameObject> Plants;

        public void Add(GameObject item) =>
            Selecting(item).Add(item);

        public void Remove(GameObject item) =>
            Selecting(item).Remove(item);


        public List<GameObject> Selecting(GameObject item)
        {
            UnitFacade facade = item.GetComponent<UnitFacade>();
            foreach (string a in facade.unitType) return
                    List(a);

            return null;
        }

        public GameObject Get(string tag, int id)
        {
            if (id >= List(tag).Count) id = 0;
            return List(tag)[id];
        }

        public int GetLenght(string tag) => List(tag).Count;

        public List<GameObject> List(string tag)
        {
            if (tag == "Yard") return Yards;
            if (tag == "Barrack") return Barracks;
            if (tag == "Factory") return Fabrics;
            if (tag == "Aerodroms") return Aerodroms;
            if (tag == "Generator") return Generators;
            if (tag == "Plant") return Plants;
            return null;
        }

        public bool HasBuild()
        {
            List<GameObject> Append = new List<GameObject>();

            Append.AddRange(Yards);
            Append.AddRange(Barracks);
            Append.AddRange(Fabrics);
            Append.AddRange(Aerodroms);
            Append.AddRange(Generators);
            Append.AddRange(Plants);

            return Append.Count > 0;
        }
    }
}
