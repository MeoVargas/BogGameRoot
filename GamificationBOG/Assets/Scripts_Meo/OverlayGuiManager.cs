using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverlayGuiManager : MonoBehaviour
{
	public GameObject profilePanel;
	public GameObject badgeInventory;
	public GameObject mainPanelButtons;
	public GameObject contributionFeed;
	public bool inventoryMode;

	public void TougleInventoryMode()
	{
		if (!inventoryMode)
		{
			inventoryMode = true;
			badgeInventory.SetActive(true);
			mainPanelButtons.SetActive(false);
			contributionFeed.SetActive(false);


		}
		else
		{
			inventoryMode = false;
			badgeInventory.SetActive(false);
			mainPanelButtons.SetActive(true);
			contributionFeed.SetActive(true);
		}
	}

}
