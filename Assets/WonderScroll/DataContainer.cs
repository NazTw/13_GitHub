using UnityEngine;
using System;
using System.Collections.Generic;

public interface IDataContainer
{
    RectTransform Show(int id);

    void Hide(int id);

    Type GetDataType();

    int Count { get; }

    int NewDataCount { get; set; }

    Vector2 BasicViewSize { get; }
}

public interface IDataContainer<T> : IDataContainer
{
    void Add(T data);
}

public class DataContainer<T> : IDataContainer<T>
{
    private bool _dirty;

    private IViewItem<T> _prefab;

    private List<T> _dataSources;

    private List<IViewItem<T>> _viewItemList;

    private HashSet<IViewItem<T>> _activeViewItems;

    private HashSet<IViewItem<T>> _closingViewItems;

    private Dictionary<int, IViewItem<T>> _dicDataPair;

    private Vector2 _basicSize;

    public int Count { get { return _dataSources.Count; } }

    public int NewDataCount { get; set; }

    public Vector2 BasicViewSize { get { return _basicSize; } }

    public DataContainer(IViewItem<T> prefab)
    {
        _prefab = prefab;
        _basicSize = prefab.rectTransform.sizeDelta;
        _dataSources = new List<T>();
        _dicDataPair = new Dictionary<int, IViewItem<T>>();
        _viewItemList = new List<IViewItem<T>>();
        _activeViewItems = new HashSet<IViewItem<T>>();
        _closingViewItems = new HashSet<IViewItem<T>>();
    }

    public void Add(T data)
    {
        _dataSources.Add(data);
        NewDataCount++;
    }

    public RectTransform Show(int id)
    {
        if (_dicDataPair.ContainsKey(id))
            return _dicDataPair[id].rectTransform;
        IViewItem<T> item = GetViewItem();
        item.DisplayScrollItem(_dataSources[id]);
        _dicDataPair.Add(id, item);
        _activeViewItems.Add(item);
        if (_closingViewItems.Contains(item))
            _closingViewItems.Remove(item);
        _dirty = true;
        item.rectTransform.gameObject.SetActive(true);
        return item.rectTransform;
    }

    public void Hide(int id)
    {
        if (!_dicDataPair.ContainsKey(id))
            return;
        IViewItem<T> item = _dicDataPair[id];
        _dicDataPair.Remove(id);
        _activeViewItems.Remove(item);
        _closingViewItems.Add(item);
        _dirty = true;
    }

    public void Update()
    {
        if (!_dirty)
            return;
        var enumerator = _closingViewItems.GetEnumerator();
        while(enumerator.MoveNext())
        {
            enumerator.Current.rectTransform.gameObject.SetActive(false);
        }
        enumerator.Dispose();

        _dirty = false;
    }

    public Type GetDataType() { return typeof(T); }

    private IViewItem<T> GetViewItem()
    {
        IViewItem<T> result = null;
        for (int x = 0; x < _viewItemList.Count; x++)
        {
            if (_activeViewItems.Contains(_viewItemList[x]))
                continue;
            result = _viewItemList[x];
        }

        if(result == null)
        {
            RectTransform prefab = _prefab.rectTransform;
            GameObject lastCreated = MonoBehaviour.Instantiate<RectTransform>(prefab, prefab.parent).gameObject;
            result = lastCreated.GetComponent<IViewItem<T>>();
            _viewItemList.Add(result);
        }

        return result;
    }
}
