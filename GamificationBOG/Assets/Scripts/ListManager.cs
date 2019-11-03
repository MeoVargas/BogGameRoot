using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    public ScrollRect ScrollPanel;
    public RectTransform Item;
    public int count = 50;
    public bool gridStyle = false;

    public enum ItemType { Category, Group, Contributor }

    public void AddItemsToList()
    {
        if (!gridStyle)
        {
            Vector2 startPosition = new Vector2(0f, Item.rect.height * -0.5f);

            for (int i = 0; i < count; i++)
            {
                ListItem listItem = Instantiate(Item.gameObject, ScrollPanel.content.transform).GetComponent<ListItem>();
                listItem.id = i;

                Vector2 itemPosition = new Vector2(startPosition.x, startPosition.y - i * Item.rect.height);

                listItem.GetComponent<RectTransform>().anchoredPosition = itemPosition;

                ScrollPanel.content.sizeDelta = new Vector2(0, ScrollPanel.content.rect.height + Item.rect.height);
            }
        }
        else
        {
            int horItemCount = Mathf.FloorToInt(ScrollPanel.content.rect.width / Item.rect.width);
            int rowCount = Mathf.CeilToInt(count / horItemCount);
            ScrollPanel.content.sizeDelta = new Vector2(0, (rowCount + 1) * Item.rect.height);
            ScrollPanel.content.anchoredPosition = Vector2.zero;

            for(int i = 0; i < count; i++)
            {
                ListItem listItem = Instantiate(Item.gameObject, ScrollPanel.content.transform).GetComponent<ListItem>();
                listItem.id = i;

                int currentRow = RowNumber(horItemCount, i);
                int currentCol = ColumnNumber(horItemCount, i);

                float horizontalStep = (ScrollPanel.content.rect.width / horItemCount) * 0.5f;
                float horOffset = currentCol == 1 ? horizontalStep : (2 * currentCol - 1) * horizontalStep;

                listItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(ScrollPanel.content.rect.width * -0.5f + horOffset, -currentRow * Item.rect.height);
            }
        }
    }

    private int RowNumber(int CountInRow, int CurrentIndex)
    {
        return (int)Mathf.Ceil((CurrentIndex + 1f) / CountInRow);
    }

    private int ColumnNumber(int CountInRow, int CurrentIndex)
    {
        if (CurrentIndex + 1 < CountInRow)
            return CurrentIndex + 1;
        else
        {
            int rem = (CurrentIndex + 1) % CountInRow;

            if (rem > 0)
                return rem;
            else
                return CountInRow;
        }
    }

    public void ClearList()
    {
        Transform[] children = ScrollPanel.content.transform.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child != ScrollPanel.content.transform)
                Destroy(child.gameObject);
        }

        ScrollPanel.content.sizeDelta = Vector2.zero;
    }
}
