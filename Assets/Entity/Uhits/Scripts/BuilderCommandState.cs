namespace RTS
{
    public abstract class BuilderStates
    {
        protected readonly BuildQueue queue;
        protected readonly BuildCommandsStruct str;

        public BuilderStates(BuildQueue q, BuildCommandsStruct s)
        {
            queue = q;
            str = s;
        }

        public abstract void Start();
        public abstract void Undo();
    }


    public class BuilderStateIdle : BuilderStates
    {
        public BuilderStateIdle(BuildQueue q, BuildCommandsStruct s) : base(q, s) { }

        public override void Start() =>
            queue.BuildAdd(str.unit, str.time);

        public override void Undo() { }
    }

    public class BuilderStateProcess : BuilderStates
    {
        public BuilderStateProcess(BuildQueue q, BuildCommandsStruct s) : base(q, s) { }

        public override void Start() =>
            queue.BuildAdd(str.unit, str.time);

        public override void Undo() =>
            queue?.timer?.SetPause(true);
    }
    
    public class BuilderStatePause : BuilderStates
    {
        public BuilderStatePause(BuildQueue q, BuildCommandsStruct s) : base(q, s) { }

        public override void Start() =>
            queue?.timer?.SetPause(false);

        public override void Undo() =>
            queue.BuildRemove(str);
    }
}
