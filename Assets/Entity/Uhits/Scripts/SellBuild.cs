using UnityEngine;
using Zenject;

namespace RTS
{
    public class SellBuild : MonoBehaviour
    {
        [SerializeField] private float coef = 0.8f;
        private PlayerResources playerResources;

        private float price => GetComponent<UnitFacade>().cost;
        private int team => GetComponent<UnitTeam>().team;
        private HealthSystem health => GetComponent<HealthSystem>();


        [Inject]
        public void GetMoney(PlayerResources[] r)
        {
            playerResources = r[team - 1];
        }

        public void Sell(int t) 
        {
            if(team != t) return;
            playerResources.ChangeMoney(price * coef * health.percent);
            health.Death();
        } 
    }
}
