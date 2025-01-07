using UnityEngine;

public class MapMarker : MonoBehaviour
{
    [SerializeField] GameObject marker;

    public void SetColor(Color c) =>
        marker.GetComponent<SpriteRenderer>().color = c;
}
