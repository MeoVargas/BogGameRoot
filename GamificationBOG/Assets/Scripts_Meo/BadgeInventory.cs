using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BadgeInventory : MonoBehaviour
{
	public GameObject slotGrid;
	public List<InventorySlot> inventorySlots;
	public InventorySlot[] activeSlots;
	public GameObject badgeForSpawn;

	public GameObject heldBadge;
	public GameObject occupiedSlot;

	public Badge selectedBadge;

	//SlotVariables
	int hoverdSlot;

	//DescriptionPanelVariables
	public TextMeshProUGUI badgeNameText;
	public TextMeshProUGUI badgeDescriptionText;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			AddRandomBadge();
		}
	}

	void Start()
    {
		InitilizeSlots();
    }

	void AddRandomBadge()
	{
		for (int i = 0; i < inventorySlots.Count; i++)
		{
			if (!inventorySlots[i].occupied)
			{
				// Spawn Badge Object
				BadgeObject newBagde = Instantiate(badgeForSpawn, inventorySlots[i].transform.position, inventorySlots[i].transform.rotation).GetComponent<BadgeObject>();
				newBagde.transform.SetParent(inventorySlots[i].transform);
				// Change Badge Object Parameters
				newBagde.badge.slotID = inventorySlots[i].ID;
				int randomBadgeName = Random.Range(0, System.Enum.GetValues(typeof(Badge.BadgeName)).Length);
				int randomBadgeRank = Random.Range(0, System.Enum.GetValues(typeof(Badge.BadgeRank)).Length);
				string blankDescription = (Badge.BadgeName)randomBadgeName + " Is Very Effective Badge";
				int randomBooster = Random.Range(0, 3);
				newBagde.InitialiseBadge((Badge.BadgeName)randomBadgeName, (Badge.BadgeRank)randomBadgeRank, blankDescription, randomBooster);

				//Change Slot Paratmeters
				inventorySlots[i].occupied = true;
				inventorySlots[i].instertedBadge = newBagde.gameObject;
				break;
			}
		}

	}
	void InitilizeSlots()
	{
		for (int i = 0; i < slotGrid.transform.childCount; i++)
		{
			inventorySlots.Add(slotGrid.transform.GetChild(i).GetComponent<InventorySlot>());
			inventorySlots[i].ID = i;
			inventorySlots[i].Inventory = this;
		}
	}
	public void HovereSlot(int slotID)
	{
		hoverdSlot = slotID;
	}
	public bool CheckIfAnySlotIsHovered()
	{
		for (int i = 0; i < inventorySlots.Count; i++)
		{
			if (inventorySlots[i].hovered)
			{
				return true;
			}
		}
		for (int i = 0; i < activeSlots.Length; i++)
		{
			if (inventorySlots[i].hovered)
			{
				return true;
			}
		}
		return false;
	}
	public void DeselectSlots(GameObject exeptionObject)
	{
		for (int i = 0; i < inventorySlots.Count; i++)
		{
			if (inventorySlots[i].name != exeptionObject.name)
			{
				inventorySlots[i].DeselectSlot();
			}
		}
		for (int i = 0; i < activeSlots.Length; i++)
		{
			if (activeSlots[i] != exeptionObject)
			{

				activeSlots[i].DeselectSlot();
			}

		}
	}
	public void DrawSelectedBadgeInfo(Badge selectedBadge)
	{
		badgeNameText.text = selectedBadge.badgeName.ToString();
		badgeDescriptionText.text = selectedBadge.badgeDescription;
	}
}
