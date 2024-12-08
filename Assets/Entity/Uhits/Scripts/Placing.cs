using UnityEngine;
using System.Collections;
using Zenject;

namespace RTS
{
    public class Placing : MonoBehaviour // Placement of units on the map
    {
        private bool placing;
        private Vector3 start, half;
        private SelectionBorder selectionBorder;
        [SerializeField] private SpriteRenderer placeMarker;

        public delegate void PlaceDelegate(GameObject unit);
        public event PlaceDelegate PlaceEvent;

        [Inject]
        public void Construct(SelectionBorder sb) => selectionBorder = sb;

        public void SetPlacing(bool b)
        {
            placing = b;
            selectionBorder.canSelection = !b;
            placeMarker.gameObject.SetActive(b);
        }

        void Start()
        {
            half = Vector3.Scale(GetComponent<BoxCollider>().size, transform.localScale) / 2;
        }

        void Update()
        {
            if (!placing) return;
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

        private IEnumerator Click()
        {
            yield return new WaitForSeconds(Time.deltaTime);

            SetPlacing(false);
            PlaceEvent?.Invoke(gameObject);
            Destroy(placeMarker.gameObject);
            Destroy(this);
        }

        private void Remove()
        {
            if (Input.GetMouseButtonDown(1))
                Destroy(gameObject);
        }

        private bool CanPlace()
        {
            start = gameObject.transform.position + GetComponent<BoxCollider>().center;
            Collider[] hitColliders = Physics.OverlapBox(start, half);// ,11111111 << 7 , ~(001 << 7)
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.tag == "Unit" && collider.gameObject != gameObject)
                {
                    placeMarker.color = Color.red;
                    return false;
                }
            }

            placeMarker.color = Color.green;
            return true;
        }
    }


}