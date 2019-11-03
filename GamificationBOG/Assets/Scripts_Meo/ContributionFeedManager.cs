using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContributionFeedManager : MonoBehaviour
{
	public GameObject contributionPanelToSpawn;
	public FundContributionPanel fundContributionPanel;
	public RectTransform sliderContent;
	public float xPedding;
	public float yPedding;
	float yOffset;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			SpawnContributionPanel();
		}
	}

	void SpawnContributionPanel()
	{
		yOffset += yPedding;
		Vector2 spawnPointPosition = new Vector2(sliderContent.position.x + xPedding, sliderContent.position.y - yOffset);
		fundContributionPanel = Instantiate(contributionPanelToSpawn, spawnPointPosition, sliderContent.rotation).GetComponent<FundContributionPanel>();
		yOffset += fundContributionPanel.GetComponent<RectTransform>().sizeDelta.y;
		sliderContent.sizeDelta = new Vector3 (sliderContent.localScale.x, sliderContent.localScale.y + yOffset, sliderContent.localScale.z);
		fundContributionPanel.transform.SetParent(sliderContent);
		int randomContributionType = Random.Range(0, System.Enum.GetValues(typeof(FundContribution.ContributionType)).Length);
		string contributionDescription = "Important Contribution";
		int randomContributionSum = Random.Range(0, 100);
		fundContributionPanel.InitializeContributionVisuals((FundContribution.ContributionType)randomContributionType, contributionDescription, randomContributionSum);
	}
}
