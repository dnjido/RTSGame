using RTS;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    public class BuildSystem
    {
        List<BuildQueue> queues = new List<BuildQueue>();
    }

    public struct BuildCommandsStruct
    {
        public BuildCommand command;
        public Timer timer;
    }

    public class BuildQueue
    {
        List<BuildCommandsStruct> build = new List<BuildCommandsStruct>();
        BuildCommandsStruct buildCommandsStruct;

        public void BuildEnd()
        {
            build.RemoveAt(0);
            if (build.Count > 0) BuildStart();
        }

        public void BuildStart()
        {
            build[0].command.Command();
        }

        public void BuildAdd(BuildCommandsStruct c)
        {
            build.Add(c);
        }

        public void BuildRemove(BuildCommandsStruct c)
        {
            build.Remove(c);
        }

        public void CreateButton(GameObject panel)
        {

        }
    }
}
