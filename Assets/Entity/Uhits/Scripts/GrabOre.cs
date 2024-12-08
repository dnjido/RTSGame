using UnityEngine;
using Zenject;

namespace RTS
{
    public interface IHarvest // Convert resources into money
    {
        public Vector3 point { get; }
        public GameObject currentHarvester { get; set; }
        void SetHarvester(GameObject h);
        void ClearHarvester();
    }

    public class GrabOre : MonoBehaviour, IHarvest
    {
        public GameObject currentHarvester { get; set; }
        [SerializeField] private GameObject deployPoint;

        private PlayerResources[] playerResources;
        private PlayerResources currentResource => 
            playerResources[GetComponent<UnitTeam>().team - 1];

        [Inject]
        public void GetResource(PlayerResources[] r) => 
            playerResources = r;

        public Vector3 point => deployPoint.transform.position;

        public void Grab(float c)
        {
            currentResource.ChangeMoney((int)-c);
        }

        public void SetHarvester(GameObject h)
        {
            currentHarvester = h;
            currentHarvester.GetComponent<OreMining>().HarvestEvent += Grab;
        }

        public void ClearHarvester()
        {
            currentHarvester.GetComponent<OreMining>().HarvestEvent -= Grab;
            currentHarvester = null;
        }
    }
}
