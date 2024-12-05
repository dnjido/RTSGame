using DG.Tweening;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.1f;
    private void Update()
    {
        Rotate();
    }
    public void Rotate()
    {
        Camera cam = Camera.main;
        Vector2 positionOnScreen = cam.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = cam.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        transform.DORotate(new Vector3(0, -angle - 90, 0), rotationSpeed).SetEase(Ease.OutQuad);
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b) => Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;

}