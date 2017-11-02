using UnityEngine;
using System.Collections;

public class TestItem1 : MonoBehaviour, IViewItem<float>
{
    public RectTransform rectTransform
    {
        get
        {
            return GetComponent<RectTransform>();
        }
    }

    public void DisplayScrollItem(float data)
    {
        Debug.Log(name + ": " + data);
    }
}
