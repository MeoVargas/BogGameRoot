using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeObject : MonoBehaviour
{
	public Badge badge;
	public GameObject badgeIcons;
	public Color[] badgeColor;
	public Image badgeBackgroundCircle;

	private void Start()
	{
		//InitialiseBadge(Badge.BadgeName.IdeaBadge, Badge.BadgeRank.Bronze, "");
	}
	public void InitialiseBadge(Badge.BadgeName newBadgeName, Badge.BadgeRank newBadgeRank, string newBadgeDescription, int newGeneralPercentBooster)
	{
		badge.badgeName = newBadgeName;
		badge.badgeRank = newBadgeRank;
		badge.badgeDescription = newBadgeDescription;
		badge.generaPlusPointIncrementerPercent = newGeneralPercentBooster;

		for (int i = 0; i < badgeIcons.transform.childCount; i++)
		{
		  if (badgeIcons.transform.GetChild(i).transform.name == badge.badgeName.ToString())
			{
				badgeIcons.transform.GetChild(i).gameObject.SetActive(true);
			}
		  else
			{
				badgeIcons.transform.GetChild(i).gameObject.SetActive(false);
			}
		}

		badgeBackgroundCircle.color = badgeColor[(int)badge.badgeRank];

	} // Initialise Badge And Badge Visual Preferance
}
[System.Serializable]
public class Badge
{
	public enum BadgeName { IdeaBadge, LoveBadge, TechBadge, AnimalsBadge }
	public BadgeName badgeName;
	public enum BadgeRank { Bronze, Silver, Gold, Platinum, Diamond, Lion}
	public BadgeRank badgeRank;
	public string badgeDescription;
	public int slotID;
	public int generaPlusPointIncrementerPercent;

	#region(Constructors)
	public Badge()
	{

	}
	public Badge(BadgeName	newBadgeName, BadgeRank newBadgeRank, string newBadgeDescription)
	{
		badgeName = newBadgeName;
		badgeRank = newBadgeRank;
		badgeDescription = newBadgeDescription;
	}
	#endregion
}