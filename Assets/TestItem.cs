using UnityEngine;
using System.Collections;

public class TestItem : MonoBehaviour, IViewItem<int>
{
    public RectTransform rectTransform
    {
        get
        {
            return GetComponent<RectTransform>();
        }
    }

    public void DisplayScrollItem(int data)
    {
        Debug.Log(name + ": " + data);
    }
}
