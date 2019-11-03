using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
	public enum SlotType {Inventory, Inserted}
	public SlotType slotType;
	public int ID;
	public BadgeInventory Inventory;
	public GameObject holdItem;
	public GameObject slotHoveringIndicator;
	public GameObject slotSelectionObjec;
	public float slotRadius;
	public bool occupied;
	public bool hovered;
	public GameObject instertedBadge;

	OverlayGuiManager overlayGuiManager;
	MoneyTransferSimulator moneyTransferSimulator;

	private void Start()
	{
		overlayGuiManager = FindObjectOfType<OverlayGuiManager>();
		moneyTransferSimulator = FindObjectOfType<MoneyTransferSimulator>();
	}
	//Active Slots Variables
	private void Update()
	{
		if (overlayGuiManager.inventoryMode)
		{
			if (RectoContains(holdItem, gameObject))
			{
				ActivateSelection(true);
				Inventory.HovereSlot(ID);
				if (Input.GetMouseButtonDown(0))
				{
					SelectOccupiedSlot();
					PickObject();
				}
				if (Input.GetMouseButtonUp(0))
				{
					InstertObject();
				}
				if (!hovered)
				{
					hovered = true;
				}
			}
			else
			{
				ActivateSelection(false);
				if (Inventory.heldBadge != null)
				{
					if (Input.GetMouseButtonUp(0))
					{
						if (Inventory.occupiedSlot == gameObject)
						{
							if (!Inventory.CheckIfAnySlotIsHovered())
							{
								InstertObject();
							}

						}
					}
				}
				if (hovered)
				{
					hovered = false;
				}
				if (Input.GetMouseButtonDown(0))
				{
					DeselectSlot();
				}
				if (Input.GetMouseButtonUp(0))
				{
					DeselectSlot();
				}
			}
		}

	}
	bool RectoContains(GameObject object1, GameObject object2)
	{
		 RectTransform rectTransform1 = object1.GetComponent<RectTransform>();
		 RectTransform rectTransform2  = object2.GetComponent<RectTransform>();
		 return (slotRadius > Vector2.Distance(rectTransform1.position,rectTransform2.position));
	}
	void PickObject()
	{
		if (instertedBadge != null)
		{
			if (Inventory.heldBadge == null)
			{
				if (slotType == SlotType.Inserted)
				{
					BadgeObject newBadgeObject = instertedBadge.GetComponent<BadgeObject>();
					if (newBadgeObject.badge.generaPlusPointIncrementerPercent > 0)
					{
						moneyTransferSimulator.generalPercentIncrementer -= newBadgeObject.badge.generaPlusPointIncrementerPercent;
						moneyTransferSimulator.DrawBusters();
					}
				}
				instertedBadge.transform.SetParent(holdItem.transform);
				Inventory.heldBadge = instertedBadge;
				occupied = false;
				instertedBadge = null;
			}
		}
	}
	void InstertObject()
		{
			if (Inventory.heldBadge != null)
			{
				if (!occupied)
				{
				Inventory.heldBadge.transform.SetParent(transform);
				Inventory.heldBadge.transform.position = transform.position;
				instertedBadge = Inventory.heldBadge;
				Inventory.heldBadge = null;
				Inventory.occupiedSlot = gameObject;
				occupied = true;
				}

			}
			if (instertedBadge != null)
			{
				if (slotType == SlotType.Inserted)
				{
					BadgeObject newBadgeObject = instertedBadge.GetComponent<BadgeObject>();
					if (newBadgeObject.badge.generaPlusPointIncrementerPercent > 0)
					{
					moneyTransferSimulator.generalPercentIncrementer += newBadgeObject.badge.generaPlusPointIncrementerPercent;
					moneyTransferSimulator.DrawBusters();
					}
				}
			}
			
		}
	void SelectOccupiedSlot()
	{
		if (occupied)
		{
			if (!slotSelectionObjec.activeSelf)
			{
				slotSelectionObjec.SetActive(true);
				Inventory.DrawSelectedBadgeInfo(instertedBadge.GetComponent<BadgeObject>().badge);
				Inventory.DeselectSlots(gameObject);
			}
		}
	}
	public void DeselectSlot()
	{
		if (slotSelectionObjec.activeSelf)
		{
			slotSelectionObjec.SetActive(false);
		}
	}
	void ActivateSelection(bool tougle)
	{
		if (slotHoveringIndicator.activeSelf != tougle)
		{
			slotHoveringIndicator.SetActive(tougle);
		}

	}

	
}
