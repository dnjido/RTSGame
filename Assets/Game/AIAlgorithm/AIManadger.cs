using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS
{
    [Serializable]
    public class AIManadger
    {
        public MonoBehaviour monoBehaviour;
        public bool isAI;

        [SerializeField] private int team;
        [SerializeField] private AIDifficultyList difficulty = (AIDifficultyList)1;
        private AIDifficulty aiProperties;
        private AIDifficulty[] AIs;

        public int getTeam => team;
        public GameObject[] getBuildList => aiProperties.buildList;

        public FabricsList builders { get; private set; }
        public PlayerResources resources { get; private set; }

        public delegate void AIActionDelegate();
        public event AIActionDelegate AIActionEvent;

        public delegate void AIAttackDelegate();
        public event AIActionDelegate AIAttackEvent;

        public MakeUnits makeUnits;

        public void SetTeam(int t) => team = t;

        public void Init(FabricsList b, PlayerResources r, AIDifficulty[] d)
        {
            builders = b;
            resources = r;
            AIs = d;

            new MakeUnits(this);
            new MakeConstruct(this);
            new AIAttack(this);
        }

        public void Start(AIDifficultyList d)
        {
            difficulty = d;
            Start();
        }
        
        public void Start()
        {
            aiProperties = AIs[(int)difficulty];

            monoBehaviour.StartCoroutine(AIActionCoroutine());
            monoBehaviour.StartCoroutine(AIAttackCoroutine());
        }

        private IEnumerator AIActionCoroutine()
        {
            yield return new WaitForSeconds(aiProperties.reactionTime);
            Action();
            monoBehaviour.StartCoroutine(AIActionCoroutine());
        }

        private IEnumerator AIAttackCoroutine()
        {
            yield return new WaitForSeconds(aiProperties.timerAttack);
            Attack();
            monoBehaviour.StartCoroutine(AIAttackCoroutine());
        }

        private void Action() => AIActionEvent?.Invoke();
        private void Attack() => AIAttackEvent?.Invoke();
    }
}