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
        [SerializeField] private float reactionTime = 1f;
        [SerializeField] private float timerAttack = 10f;
        [SerializeField] GameObject[] buildList;

        public int getTeam => team;
        public GameObject[] getBuildList => buildList;

        public FabricsList builders { get; private set; }
        public PlayerResources resources { get; private set; }

        public delegate void AIActionDelegate();
        public event AIActionDelegate AIActionEvent;

        public delegate void AIAttackDelegate();
        public event AIActionDelegate AIAttackEvent;

        public MakeUnits makeUnits;

        public void SetTeam(int t) => team = t;

        public void Init(FabricsList b, PlayerResources r)
        {
            builders = b;
            resources = r;
            new MakeUnits(this);
            new MakeConstruct(this);
            new AIAttack(this);
            Start();
        }

        public void Start()
        {
            monoBehaviour.StartCoroutine(AIActionCoroutine());
            monoBehaviour.StartCoroutine(AIAttackCoroutine());
        }

        private IEnumerator AIActionCoroutine()
        {
            yield return new WaitForSeconds(reactionTime);
            Action();
            monoBehaviour.StartCoroutine(AIActionCoroutine());
        }

        private IEnumerator AIAttackCoroutine()
        {
            yield return new WaitForSeconds(timerAttack);
            Attack();
            monoBehaviour.StartCoroutine(AIAttackCoroutine());
        }

        private void Action() => AIActionEvent?.Invoke();
        private void Attack() => AIAttackEvent?.Invoke();
    }
}