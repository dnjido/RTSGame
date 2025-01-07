using RTS;
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
        public Relationship[] relationship;
        public MonoBehaviour monoBehaviour;
        //public readonly Relationship[] relationship;

        public HasPlaying(Relationship[] r) =>
            relationship = r;

        public void SetStatus(int id, bool a)
        {
            //print("HAS PLAYING" + playing);
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

