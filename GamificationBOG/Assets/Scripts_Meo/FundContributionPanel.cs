using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FundContributionPanel : MonoBehaviour
{
	public FundContribution fundContribution;
	public Color[] contributionTypeColors;
	public Image contributionPanelBackground;
	public TextMeshProUGUI contributionDescriptionText;
	public TextMeshProUGUI contributionSumText;

    void Start()
    {
        
    }

	public void InitializeContributionVisuals(FundContribution.ContributionType newContributionType, string newContributionDescription, int newContributionSum)
	{
		contributionPanelBackground.color = contributionTypeColors[(int)newContributionType];
		DrawContributionSumText(newContributionSum, newContributionType);
		contributionDescriptionText.text = newContributionDescription;
	}
	void DrawContributionSumText(int contributionSum, FundContribution.ContributionType newContributionType)
	{
		switch (newContributionType)
		{
			case FundContribution.ContributionType.OneTime:
				contributionSumText.text = contributionSum.ToString() + " GEL";
				break;
			case FundContribution.ContributionType.Subscribtion:
				contributionSumText.text = "Month/" + contributionSum.ToString() + " GEL";
				break;
			case FundContribution.ContributionType.OnEveryPay:
				contributionSumText.text = contributionSum.ToString() + " GEL" + " Per Transaction";
				break;
		}
	}
}
[System.Serializable]
public class FundContribution
{
	public enum ContributionType {OneTime, Subscribtion, OnEveryPay}
	public ContributionType contributionType;
	public string contributionDescription;
	public int contributionSum;
	public int contributionProgression;
	public int contributionMaxProgression;
}
