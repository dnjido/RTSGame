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
        private SelectedUnits selectedUnits;
        public bool canSelection = true;

        [Inject]
        public void Construct(SelectedUnits su) => selectedUnits = su;

        void Start()
        {
            //if (!selectionBox) return;

            rectTransform = selectionBox.GetComponent<RectTransform>();
            positions = new BorderPos();
        }

        // Update is called once per frame
        void Update()
        {
            if (!canSelection) return;
            if (Input.GetMouseButtonDown(0)) StartSelection();
            if (isSelecting) Selecting();
            if (Input.GetMouseButtonUp(0)) StopSelection();
        }

        private void StartSelection()
        {
            if (CursorOnUI.CursorOverUI()) return;

            selectedUnits.ClearUnits();
            isSelecting = true;
            startMousePosition = CursorPosition.LocalPos(cam, canvas);
            selectionBox.gameObject.SetActive(true);
            rectTransform.anchoredPosition = startMousePosition;
            rectTransform.sizeDelta = Vector2.zero;
            positions.pos1 = CursorRay.RayPoint();
        }

        private void Selecting()
        {
            positions.pos2 = CursorRay.RayPoint();
            Vector2 currentMousePosition = CursorPosition.LocalPos(cam, canvas);
            Vector2 size = currentMousePosition - startMousePosition;
            rectTransform.sizeDelta = new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
            rectTransform.anchoredPosition = startMousePosition + size / 2;
            SetOverlap(positions.pos1, positions.pos2);
        }

        private void StopSelection()
        {
            isSelecting = false;
            selectionBox.gameObject.SetActive(false);
        }

        private void SetOverlap(Vector3 pos1, Vector3 pos2)
        {
            Vector3 center = (pos1 + pos2) / 2;
            float halfx = Mathf.Abs(pos1.x - pos2.x) / 2;
            float halfz = Mathf.Abs(pos1.z - pos2.z) / 2;
            Vector3 half = new Vector3(halfx, 20, halfz);

            Collider[] hitColliders = Physics.OverlapBox(center, half, Quaternion.identity, 11111111 << 7);// , 1 << 6
            selectedUnits.AddUnits(hitColliders);
        }
    }
}