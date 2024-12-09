using RTS;
using UnityEngine;
using Zenject;
using System.Linq;
using System.Collections.Generic;

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

        public static DetectEnemy Detector(GameObject unit)
        {
            try { return unit.GetComponent<DetectEnemy>(); }
            catch { return unit.AddComponent<DetectEnemy>(); }
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

        //public static T NullObjectt<T>() where T : class
        //{
        //    try { return new T() as class; }
        //    catch { return unit.AddComponent<T>(); }
        //}
    }
}