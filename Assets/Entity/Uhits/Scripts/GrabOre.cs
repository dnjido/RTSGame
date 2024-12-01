using UnityEngine;

namespace RTS
{
    public interface IHarvest // Convert resources into money
    {
        public Vector3 point { get; }
        public GameObject currentHarvester { get; set; }
    }

    public class GrabOre : MonoBehaviour, IHarvest
    {
        public GameObject currentHarvester { get; set; }
        [SerializeField] private GameObject deployPoint;

        public Vector3 point => deployPoint.transform.position;

        public void Grab(float c)
        {

        }
    }
}
