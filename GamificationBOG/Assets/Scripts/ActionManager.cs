using UnityEngine;

[RequireComponent(typeof(Panel))]
public class ActionManager : MonoBehaviour
{
    public Panel ScrollPanel;
    public Panel GroupInfo;
    public ListManager Categories;
    public ListManager Groups;
    public ListManager Contributors;

    public GameObject mainUIPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void ShowCategories()
    {
        ScrollPanel.Show();
        Categories.AddItemsToList();
        mainUIPanel.SetActive(false);
    }

    public void HideActivePanel()
    {
        ScrollPanel.Hide();
        Categories.ClearList();
    }

    private void ShowCategoryGroups(int Id)
    {
        Categories.ClearList();
        Groups.AddItemsToList();

    }

    public void ShowGroupPanel(int Id)
    {
        ScrollPanel.Hide();
        Groups.ClearList();
        GroupInfo.Show();
        Contributors.AddItemsToList();
    }

    public void OnListItemClicked(ListItem Sender)
    {
        if (Sender.Type == ListManager.ItemType.Category)
            ShowCategoryGroups(Sender.id);
        else if(Sender.Type == ListManager.ItemType.Group)
            ShowGroupPanel(Sender.id);
    }
}
