using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
    public int id = 0;
    public Image image;
    public string caption;
    public string data;

    public ListManager.ItemType Type;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => FindObjectOfType<ActionManager>().OnListItemClicked(this));
    }
}
