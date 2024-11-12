using UnityEngine;
using Zenject;

namespace RTS
{
    struct BorderPos
    {
        public Vector3 pos1;
        public Vector3 pos2;
    }

    public class SelectionBorder : MonoBehaviour // Renders a frame and creates a collider for selection.
    {
        [SerializeField] private bool isSelecting;
        [SerializeField] private Vector2 startMousePosition;
        [SerializeField] private GameObject selectionBox;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Canvas canvas;
        [SerializeField] private Camera cam;
        private BorderPos positions;
        private CursorRay ray;
        private CursorPosition cPos;
        private SelectedUnits selectedUnits;

        [Inject]
        public void Construct(SelectedUnits su) => selectedUnits = su;

        void Start()
        {
            if (!selectionBox) return;

            rectTransform = selectionBox.GetComponent<RectTransform>();
            positions = new BorderPos();
            ray = new CursorRay();
            cPos = new CursorPosition(cam, canvas);
        }

        // Update is called once per frame
        void Update()
        {
            if (!selectionBox) return;

            if (Input.GetMouseButtonDown(0)) StartSelection();
            if (isSelecting) Selecting();
            if (Input.GetMouseButtonUp(0)) StopSelection();
        }

        private void StartSelection()
        {
            selectedUnits.ClearUnits();
            isSelecting = true;
            startMousePosition = cPos.LocalPos();
            selectionBox.gameObject.SetActive(true);
            rectTransform.anchoredPosition = startMousePosition;
            rectTransform.sizeDelta = Vector2.zero;
            positions.pos1 = ray.Ray();
        }

        private void Selecting()
        {
            positions.pos2 = ray.Ray();
            Vector2 currentMousePosition = cPos.LocalPos();
            Vector2 size = currentMousePosition - startMousePosition;
            rectTransform.sizeDelta = new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
            rectTransform.anchoredPosition = startMousePosition + size / 2;
            SetOverlap(positions.pos1, positions.pos2);
        }

        private void StopSelection()
        {
            isSelecting = false;
            selectionBox.gameObject.SetActive(false);
            //print(selectedUnits.units.Count);
        }

        private void SetOverlap(Vector3 pos1, Vector3 pos2)
        {
            Vector3 center = (pos1 + pos2) / 2;
            float halfx = Mathf.Abs(pos1.x - pos2.x);
            float halfz = Mathf.Abs(pos1.z - pos2.z);
            Vector3 half = new Vector3(halfx, 20, halfz);

            Collider[] hitColliders = Physics.OverlapBox(center, half, Quaternion.identity);
            selectedUnits.AddUnits(hitColliders);
        }
    }
}