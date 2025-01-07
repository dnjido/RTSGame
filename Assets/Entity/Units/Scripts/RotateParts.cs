using DG.Tweening;
using UnityEngine;

public class RotateParts : MonoBehaviour
{
    [SerializeField] private GameObject[] parts;
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] private float rotationAngle;
    [SerializeField] private float duration;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var part in parts)
        {
            part.transform.DORotate(rotationAxis * rotationAngle, duration, RotateMode.LocalAxisAdd)
                     .SetLoops(-1, LoopType.Incremental);
        }
    }
}
