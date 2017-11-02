using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public enum ScrollDirection
{
    Horizontal,
    Vertical
}

public class WonderScrollLayout : WonderScrollBase, IDragHandler
{
    [SerializeField]
    private ScrollDirection _direction;

    [SerializeField]
    private RectOffset _padding;

    [SerializeField]
    private float _spacing = 0;

    [SerializeField]
    private int _unitPerLine = 1;

    //全部資料的大小
    private Vector2 _scrollViewSize;

    private List<Vector2> _listDataPosition;

    private Vector2 _lastPosition;

    private Vector2 _delta;

    private Type _lastType;

    private Vector2 _lastSize;

    private int _row;

    private int _col;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset = eventData.delta;
        if (_direction == ScrollDirection.Horizontal)
        {
            offset.y = 0;
        }
        else
        {
            offset.x = 0;
        }

        _delta += offset;
    }

    protected override void Awake()
    {
        base.Awake();
        _listDataPosition = new List<Vector2>();
        _scrollViewSize = new Vector2(_padding.horizontal, _padding.vertical);
        _lastPosition = new Vector2(_padding.left, -_padding.top);
        _delta = Vector2.zero;
        _row = 0;
        _col = 0;

        _visibleData.AddRange(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
    }

    protected override void DisplayItems()
    {
        _visibleData.Sort(Order);
        _containerFilter.Show(_visibleData, ref _visibleItems);
        for (int x = 0; x < _visibleData.Count; x++)
        {
            int dataId = _visibleData[x];
            _visibleItems[x].localPosition = _listDataPosition[dataId] + _delta;
        }
    }

    protected override void OnAddContainer(IDataContainer container)
    {
        bool differentType = _lastType != container.GetDataType();
        if(differentType && _row != 0)
        {
            _row++;
            _col = 0;
        }

        Vector2 basicSize = container.BasicViewSize;
        for (int x = 0; x < container.NewDataCount; x++)
        {
            float px = _lastPosition.x;
            float py = _lastPosition.y;
            
            if(_direction == ScrollDirection.Horizontal)
            {
                if (_col != 0)
                {
                    py -= _spacing + basicSize.y;
                }
                else
                {
                    py = -_padding.top;

                    if (_row != 0)
                    {
                        if (!differentType)
                        {
                            px += _spacing + basicSize.x;
                        }
                        else
                        {
                            differentType = false;
                            px += _spacing + _lastSize.x;
                        }
                    }
                }
            }
            else
            {
                if (_col != 0)
                {
                    px += _spacing + basicSize.x;
                }
                else
                {
                    px = _padding.left;

                    if (_row != 0)
                    {
                        if(!differentType)
                        {
                            py -= _spacing + basicSize.y;
                        }
                        else
                        {
                            differentType = false;
                            py -= _spacing + _lastSize.y;
                        }
                    }
                }
            }

            Vector2 position = new Vector2(px, py);
            _lastPosition = position;
            _listDataPosition.Add(position);

            _col++;
            if (_col == _unitPerLine)
            {
                _row++;
                _col = 0;
            }
        }

        container.NewDataCount = 0;

        _lastType = container.GetDataType();
        _lastSize = basicSize;
    }
}
