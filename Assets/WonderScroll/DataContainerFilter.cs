using UnityEngine;
using System.Collections.Generic;

public class DataContainerFilter
{
    private IDataContainer _lastContainer;

    private List<IDataContainer> _listContainer;

    public IDataContainer lastContainer { get { return _lastContainer; } }

    public DataContainerFilter()
    {
        _listContainer = new List<IDataContainer>();
    }

    public IDataContainer AddContainer<T>(IViewItem<T> viewItem)
    {
        IDataContainer container = new DataContainer<T>(viewItem);
        _listContainer.Add(container);
        _lastContainer = container;
        return container;
    }

    public void Show(List<int> itemsId, ref List<RectTransform> visibleRectTrans)
    {
        if (itemsId.Count == 0)
            return;
        visibleRectTrans.Clear();
        int sum = 0;
        int showedIndex = 0;
        for (int x = 0; x < _listContainer.Count; x++)
        {
            IDataContainer container = _listContainer[x];
            for (int y = showedIndex; y < itemsId.Count; y++)
            {
                int id = itemsId[y];
                if (sum + container.Count <= id)
                {
                    sum += container.Count;
                    break;
                }

                int index = id - sum;
                visibleRectTrans.Add(container.Show(index));

                showedIndex++;
            }

            if (showedIndex == itemsId.Count)
                return;
        }

        throw new System.Exception("BUG");
    }

    public void Hide(int id)
    {

    }
}