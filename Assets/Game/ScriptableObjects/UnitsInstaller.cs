using RTS;
using UnityEngine;
using Zenject;
using System.Linq;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "UnitsInstaller", menuName = "Installers/UnitsInstaller")]
public class UnitsInstaller : ScriptableObjectInstaller<UnitsInstaller>
{
    public UnitsStats[] units;
    public UnitsStats[] builds;
    public override void InstallBindings()
    {
        int c = 0;
        Dictionary<int, int> lengths = new Dictionary<int, int>()
        {
            [0] = units.Length,
            [1] = builds.Length
        };

        UnitsStats[] combined = units.Concat(builds).ToArray();
        int[] firstIndexes = new int[lengths.Count];


        for (int i = 0; i < lengths.Count; i++) 
        {
            firstIndexes[i] = c;
            c += lengths[i];
        }

        Container.BindInstances(new GetStats(combined, firstIndexes));
    }
}

namespace RTS
{
    public enum StatsCategoryList
    {
        Units = 0,
        Builds = 1,
    };

    public class GetStats
    {
        readonly UnitsStats[] combined;
        readonly int[] firstIndexes;

        public UnitsStats Stats(GameObject unit)
        {
            try
            {
                (int, int) ids = unit.GetComponent<UnitFacade>().GetIDs();
                int firstIndex = firstIndexes[ids.Item2];
                return combined[firstIndex + ids.Item1];
            }
            catch { return new UnitsStats(); }
        }

        public UnitsStats Stats(int l, int id)
        {
            int firstIndex = firstIndexes[l];
            return combined[firstIndex + id];
        }

        public GetStats(UnitsStats[] u, int[] fi)
        {
            combined = u;
            firstIndexes = fi;
        }
    }



    public class GU
    {
        public static int Team(GameObject unit)
        {
            try { return unit.GetComponent<UnitTeam>().team; }
            catch { return 1; }
        }

        public static int Relationship(GameObject unit)
        {
            try { return unit.GetComponent<UnitTeam>().relationship; }
            catch { return 0; }
        }

        public static DetectEnemy Detector(GameObject unit)
        {
            try { return unit.GetComponent<DetectEnemy>(); }
            catch { return unit.AddComponent<DetectEnemy>(); }
        }

        public static UnitFacade Facade(GameObject unit)
        {
            try { return unit.GetComponent<UnitFacade>(); }
            catch { return unit.AddComponent<UnitFacade>(); }
        }

        public static string[] Attribute(GameObject unit)
        {
            try { return unit.GetComponent<UnitFacade>().unitType; }
            catch { return new string[0]; }
        }

        public static bool HasAttribute(GameObject unit, string attr)
        {
            try { return Attribute(unit).Any(a => a == attr); }
            catch { return false; }
        }

        public static bool EqualAttribute(GameObject unit1, GameObject unit2)
        {
            try { return Attribute(unit1).Any(a => Attribute(unit2).Contains(a)); }
            catch { return false; }
        }

        public static T NullComponent<T>(GameObject unit) where T : Component
        {
            try { return unit.GetComponent<T>(); }
            catch { return unit.AddComponent<T>(); }
        }

        public static T NullComponent<T>(Component comp) where T : Component
        {
            GameObject unit = comp.gameObject;
            try { return unit.GetComponent<T>(); }
            catch { return unit.AddComponent<T>(); }
        }
    }

    public class NO
    {
        public static T NullObjectt<T>(object c) where T : new()
        {
            try { return (T)Convert.ChangeType(c, typeof(T)); }
            catch { return new T(); }
        }
    }

    public static class GetAttr
    {
        public static bool GetAttribute(GameObject unit, string attr)
        {
            foreach (string a in GU.Attribute(unit))
                if (a == attr) return true;

            return false;
        }

        public static List<GameObject> GetUnitsWihAttr(GameObject[] units, string attr)
        {
            List<GameObject> list = new List<GameObject>();

            foreach (GameObject u in units)
                if (GetAttribute(u, attr))
                    list.Add(u);

            return list;
        }
    }
}