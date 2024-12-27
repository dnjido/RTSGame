using System;

namespace RTS
{
    [Serializable]
    public class Relationship
    {
        public Teams team;
        public int relationship { get; private set; }

        public void SetRelationship(Relationship[] r, int cur)
        {
            if (team == 0) { Add(cur); return; }
            for (int i = 0; i < r.Length; i++)
                if (r[i].team == r[cur].team) Add(i);
        }

        public void Add(int i)
        {
            int add = 0b1 << i;
            relationship = relationship | add;
        }
    }

    public enum Teams
    {
        N = 0,
        A = 1,
        B = 2,
        C = 3,
        D = 4,
    }
}