using UnityEngine;
using System.Collections;

public interface IViewItem<T>
{
    bool enabled { get; set; }

    RectTransform rectTransform { get; }

    void DisplayScrollItem(T data);
}
