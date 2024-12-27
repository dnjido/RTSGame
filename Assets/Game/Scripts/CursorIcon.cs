using UnityEngine;

public static class CursorIcon
{
    static public void Set(Texture2D t) =>
        Cursor.SetCursor(t, new Vector2(0, 0), CursorMode.Auto);

    static public void Clear() =>
        Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
}
