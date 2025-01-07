using UnityEngine;
using Zenject;

namespace RTS
{
    public class SetBuildList : MonoBehaviour // Add or remove build from building list
    {
        protected FabricsList[] fabricList;
        protected HasPlaying playing;

        public int team => GetComponent<UnitTeam>().team - 1;

        [Inject]
        public void FabricsList(FabricsList[] f) =>
            fabricList = f;

        [Inject]
        public void HasPlaying(HasPlaying p) =>
            playing = p;

        private void Start() => SetList();

        //public void SetList() =>
        //    fabricList[team].Add(gameObject);

        public void SetList() => 
            fabricList[team].Add(gameObject);


        public void Destroy()
        {
            fabricList[team].Remove(gameObject);

            if (!fabricList[team].HasBuild()) playing.SetStatus(team, false);
        }
            

        private void OnEnable()
        {
            GetComponent<HealthSystem>().DeathEvent += Destroy;
        }

        private void OnDisable()
        {
            GetComponent<HealthSystem>().DeathEvent -= Destroy;
        }
    }
}
