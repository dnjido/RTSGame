using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RTS
{
    [Serializable]
    public class HasPlaying
    {
        [SerializeField] public EndGameText gameStatusUI;
        public bool[] status = new bool[8];
        private Relationship[] relationship;
        private MonoBehaviour monoBehaviour;

        [SerializeField] private bool hasSetStatus;

        public void Init(MonoBehaviour mb, Relationship[] r, EndGameText end)
        {
            relationship = r;
            monoBehaviour = mb;
            gameStatusUI = end;
        }

        public void EnableSetStatus(bool b) => 
            hasSetStatus = b;

        public void SetStatus(int id, bool a)
        {
            if (!hasSetStatus) return;

            status[id] = a;
            PlayerStatus(id);
        }

        public void PlayerStatus(int id)
        {
            if (PlayerLose(id)) EndGame("You Lose");
            else if (PlayerWin()) EndGame("You Win");
        }

        public bool PlayerLose(int id) => id == 0;

        public bool PlayerWin() =>
            !relationship.Select((r, i) => new { r, i })
                .Any(x => relationship[0].relationship != x.r.relationship && status[x.i]);

        private void EndGame(string text)
        {
            gameStatusUI.SetText(text);
            monoBehaviour.StartCoroutine(LoadTimer());
        }

        private IEnumerator LoadTimer()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("MainMenu");
        }
    }
}

