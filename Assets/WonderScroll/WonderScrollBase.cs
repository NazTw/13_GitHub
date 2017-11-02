using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class WonderScrollBase : MonoBehaviour
{
    [SerializeField]
    protected RectTransform _viewport;

    protected DataContainerFilter _containerFilter;

    protected Dictionary<Type, GameObject> _dicTypeViewItem;

    protected Rect _viewportRect;

    protected List<int> _visibleData;

    protected List<RectTransform> _visibleItems;

    protected virtual void Awake()
    {
        _visibleData = new List<int>();
        _visibleItems = new List<RectTransform>();
        _containerFilter = new DataContainerFilter();
        _dicTypeViewItem = new Dictionary<Type, GameObject>();
        OnViewportChanged();
    }

    private void OnValidate()
    {
        if(_viewport != null)
        {
            _viewport.anchorMin = Vector2.up;
            _viewport.anchorMax = Vector2.up;
            _viewport.pivot = Vector2.up;
        }
    }

    private void Update()
    {
        if (_viewport.hasChanged)
        {
            _viewport.hasChanged = false;
            OnViewportChanged();
        }

        DisplayItems();
    }

    public void BindViewItem<T>(IViewItem<T> viewItem)
    {
        Type type = typeof(T);
        RectTransform rectTrans = viewItem.rectTransform;
        GameObject go = rectTrans.gameObject;

        rectTrans.anchorMin = Vector2.up;
        rectTrans.anchorMax = Vector2.up;
        rectTrans.pivot = Vector2.up;

        if (!_dicTypeViewItem.ContainsKey(type))
            _dicTypeViewItem.Add(type, go);
        go.SetActive(false);
    }

    public void Add<T>(T data)
    {
        IDataContainer container = _containerFilter.lastContainer;
        if (container == null || container.GetDataType() != typeof(T))
        {
            IViewItem<T> viewItem = FetchViewItem<T>();
            if (viewItem == null)
                throw new Exception("錯了  笨蛋");
            container = _containerFilter.AddContainer<T>(viewItem);
        }

        IDataContainer<T> extension = container as IDataContainer<T>;
        extension.Add(data);

        OnAddContainer(container);
    }

    protected IViewItem<T> FetchViewItem<T>()
    {
        Type type = typeof(T);
        if (!_dicTypeViewItem.ContainsKey(type))
            return null;
        return _dicTypeViewItem[type].GetComponent<IViewItem<T>>();
    }

    private void OnViewportChanged()
    {
        _viewportRect = _viewport.GetPhysicalRect();
    }

    protected abstract void DisplayItems();

    protected abstract void OnAddContainer(IDataContainer container);

    protected int Order(int a, int b) { return a.CompareTo(b); }
}
