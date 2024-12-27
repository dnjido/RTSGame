using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RTS
{
    public struct BuildCommandsStruct
    {
        public GameObject unit;
        public float time;
    }

    public class MakeBuildStruct
    {
        public static BuildCommandsStruct Make(GameObject unit, float time)
        {
            BuildCommandsStruct str = new BuildCommandsStruct();
            str.unit = unit;
            str.time = time;
            return str;
        }
    }

    public class BuildQueue // Build queue
    {
        List<BuildCommandsStruct> build = new List<BuildCommandsStruct>();
        public Timer timer { get; private set; }

        public int buildCount => build.Count;
        public GameObject currentUnit => buildCount > 0 ? build[0].unit : null;

        public delegate void StartDelegate(Timer timer, GameObject unit);
        public event StartDelegate BuildStartEvent;

        public delegate void EndDelegate(GameObject unit);
        public event EndDelegate BuildEndEvent;

        public delegate void RemoveDelegate(GameObject unit);
        public event RemoveDelegate BuildRemoveEvent;

        public delegate void AddDelegate(GameObject unit);
        public event AddDelegate BuildAddEvent;

        public delegate void SpawnDelegate(GameObject unit);
        public event SpawnDelegate BuildSpawnEvent;

        public void BuildEnd()
        {
            BuildSpawnEvent?.Invoke(build[0].unit);

            GameObject last = LastUnit(build[0].unit);
            build.RemoveAt(0);

            if (buildCount > 0) BuildStart();
            else TimerClear();

            BuildEndEvent?.Invoke(last);
        }

        public void BuildStart()
        {
            timer = new Timer(build[0].time);
            timer.TimeOutEvent += BuildEnd;

            BuildStartEvent?.Invoke(timer, build[0].unit);
        }

        public void BuildAdd(GameObject unit, float time)
        {
            BuildCommandsStruct bs = MakeBuildStruct.Make(unit,time);
            build.Add(bs);

            if (buildCount <= 1) BuildStart();

            BuildAddEvent?.Invoke(unit);
        }

        public void BuildRemove(BuildCommandsStruct c)
        {
            build.Remove(c);

            if (buildCount <= 0) TimerClear();
            else { BuildStart(); 
                timer.pause = true; }

            BuildRemoveEvent?.Invoke(LastUnit(c.unit));
        }

        public void Remove(BuildCommandsStruct c)
        {
            build.Remove(c);
            BuildRemoveEvent?.Invoke(c.unit);
        }

        public void TimerClear()
        {
            timer.TimeOutEvent -= BuildEnd;
            timer = null;
        }

        public int QueueCount(GameObject unit) => 
            build.Count(x => x.unit == unit);

        public GameObject LastUnit(GameObject unit) =>
            buildCount > 0 ? unit : null;
    }
}
