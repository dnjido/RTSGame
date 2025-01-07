using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    [Serializable]
    public class AIDifficulty
    {
        public AIDifficultyList name;
        public float reactionTime;
        public float timerAttack;
        public GameObject[] buildList;
    }

    public enum AIDifficultyList
    {
        Easy = 0,
        Medium = 1,
        Hard = 2,
    }
}