using UnityEngine;
using System.Collections;
using Zenject;
using System.Linq;
using System.Collections.Generic;

namespace RTS
{
    public class Placing : MonoBehaviour // Placement of units on the map
    {
        public IPlacedEvent placedEvent;
        private bool placing;
        private SelectionBorder selectionBorder;
        [SerializeField] private SpriteRenderer placeMarker;
        public bool hasPlaced;
        private FabricsList[] fabricList;

        int team => GetComponent<UnitFacade>().unitTr.team - 1;

        private Vector3 center => GetComponent<BoxCollider>().center;
        public Vector3 size => Vector3.Scale(GetComponent<BoxCollider>().size, transform.localScale);
        private Vector3 half => size / 2;

        [Inject]
        public void Construct(SelectionBorder sb) => 
            selectionBorder = sb;

        [Inject]
        public void Fabrics(FabricsList[] f) =>
            fabricList = f;

        public void SetPlacing(bool b)
        {
            placeMarker.gameObject.SetActive(b);
            hasPlaced = b;
            placing = b;
            selectionBorder.canSelection = !b;
        }

        void Start()
        {
            //if(!hasPlaced) 
            //    StartCoroutine(Click());
        }

        void Update()
        {
            if (!placing && !hasPlaced) return;
            Move();
            Place();
            Remove();
        }

        private void Move() =>
            transform.position = CursorRay.RayPoint();

        private void Place()
        {
            if (!CanPlace()) return;
            if (Input.GetMouseButtonDown(0) && placing) StartCoroutine(Click());
        }

        public void PlaceAI(Vector3 pos)
        {
            transform.position = pos;
            //SetPlacing(false);
            placedEvent?.Placing();
            //Activate();
            Destroy(placeMarker.gameObject);
            //StartCoroutine(Click());
        } 

        private IEnumerator Click()
        {
            yield return new WaitForSeconds(Time.deltaTime * 10);

            SetPlacing(false);
            placedEvent?.Placing();
            Activate();
            Destroy(placeMarker.gameObject);
            //Destroy(this);
        }

        private void Activate() => GetComponent<UnitActivate>()?.Activate();
        private void Deactivate() => GetComponent<UnitActivate>()?.Deactivate();

        private void Remove()
        {
            if (Input.GetMouseButtonDown(1))
                Destroy(gameObject);
        }

        private float NearYards()
        {
            List<GameObject> list = fabricList[team].List("Yard");

            return list.Max(p => Vector3.Distance(p.transform.position, transform.position));
        }

        public void RemoveAI() => Destroy(gameObject);

        private bool CanPlace()
        {
            Vector3 start = gameObject.transform.position + center;
            Collider[] hitColliders = Physics.OverlapBox(start, half);

            if (hitColliders.Any(collider =>
                (collider.gameObject.CompareTag("Unit") && collider.gameObject != gameObject) || NearYards() >= 35))
            {
                placeMarker.color = Color.red;
                return false;
            }
            placeMarker.color = Color.green;
            return true;
        }

        public bool IsAreaClear(Vector3 coord)
        {
            Vector3 start = coord;
            Collider[] hitColliders = Physics.OverlapBox(start, half);

            //return hitColliders.Any(collider => 
            //    collider.gameObject.tag == "Unit" && collider.gameObject != gameObject);

            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.tag == "Unit" && collider.gameObject != gameObject)
                    return false;
            }
            return true;
        }
    }
}