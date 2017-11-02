using UnityEngine;
using System;
using System.Collections.Generic;

public class ScrollLayoutData
{
    public Type type;

    public Vector2 size;
}

public class ScorllLayout
{
    private Rect _viewportRect;

    private List<RectTransform> _visibleTrans;

    public void SetViewport(Rect viewportRect)
    {
        _viewportRect = viewportRect;
        Reposition();
    }

    public void GetVisibleData(ref List<int> visibleList)
    {
        visibleList.Clear();
        visibleList.Add(0);
        visibleList.Add(1);
        visibleList.Add(2);
        visibleList.Add(3);
        visibleList.Add(4);
    }

    public void SetVisibleTrans(List<RectTransform> visibleTrans)
    {
        _visibleTrans = visibleTrans;
        Reposition();
    }

    private void Reposition()
    {
    }
}