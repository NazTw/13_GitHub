using UnityEngine;
using System.Collections;

public static class WonderKits
{
    public static Rect GetPhysicalRect(this RectTransform rectTransform)
    {
        Vector2 size = rectTransform.rect.size;
        Vector2 pivot = rectTransform.pivot;
        Vector3 anchoredPosition = rectTransform.position;

        Vector3 center = new Vector3
            (
                anchoredPosition.x - pivot.x * size.x,
                anchoredPosition.y - pivot.y * size.y
            );
        return new Rect(center, size);
    }
}