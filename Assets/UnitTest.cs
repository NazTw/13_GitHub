using UnityEngine;
using System.Collections;

public class UnitTest : MonoBehaviour
{
    public TestItem viewItem;

    public TestItem1 viewItem1;

    public WonderScroll wonderScroll;

    // Use this for initialization
    void Start()
    {
        wonderScroll.BindViewItem<int>(viewItem);
        wonderScroll.Add<int>(1);
        wonderScroll.Add<int>(2);
        wonderScroll.Add<int>(3);
        wonderScroll.Add<int>(4);
        wonderScroll.Add<int>(5);

        wonderScroll.BindViewItem<float>(viewItem1);
        wonderScroll.Add<float>(1);
        wonderScroll.Add<float>(2);
        wonderScroll.Add<float>(3);
        wonderScroll.Add<float>(4);
        wonderScroll.Add<float>(5);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
