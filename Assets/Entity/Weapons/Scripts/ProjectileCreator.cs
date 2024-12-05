using UnityEngine;
using Zenject;

namespace TBS
{
    public class ProjectileCreator : MonoBehaviour
    {
        //[SerializeField] private GameObject prefab;
        [SerializeField] private Transform firePoint;
        private Factory _bulletFactory;

        public class Factory : PlaceholderFactory<BulletBase> { }

        [Inject]
        public void Construct(Factory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }
        [Inject]
        private SignalBus _signalBus;
        //private ProjMoveBus _projMoveBus;

        public void Create()
        {
            BulletBase proj = _bulletFactory.Create();
            _signalBus.Fire(new ProjMoveBus(new Vector3(10, 3, 20)));

            //proj.GetComponent<ProjectileMove>().Move(new Vector3(10, 3, 20));
            //print(proj.GetComponentsInChildren<ProjectileMove>().Length);
        }
    }
}