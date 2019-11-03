using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyTransferSimulator : MonoBehaviour
{
	public int money;
	public int plusPoints;

	//Boosters
	public int generalPercentIncrementer;

	//Gui Elements
	public TMP_InputField sumInput;
	public TextMeshProUGUI moneyText;
	public Color inputFieldColor;
	public Color notEnoughFundsColor;
	public TextMeshProUGUI plusPointsText;
	public TextMeshProUGUI generalPlusPointPercentIncrimenter;



	void Start()
    {
		DrawMoney();
		DrawPlusPoints();
		DrawBusters();
	}
	void DrawMoney()
	{
		moneyText.text = money.ToString() + " GEL";
	}
	void DrawPlusPoints()
	{
		plusPointsText.text = plusPoints.ToString();
	}
	public void DrawBusters()
	{
		generalPlusPointPercentIncrimenter.text = "+" + generalPercentIncrementer.ToString() + "%";
	}
	public void MakeTransaction()
	{
		if (int.Parse(sumInput.text) <= money)
		{
			money -= int.Parse(sumInput.text);
			IncrementPlusPoints(int.Parse(sumInput.text));
			sumInput.text = null;
			DrawMoney();
			sumInput.image.color = inputFieldColor;
			
		}
		else
		{
			sumInput.image.color = notEnoughFundsColor;
		}

	}
	public void IncrementPlusPoints(int moneyTransaction)
	{
		plusPoints += moneyTransaction * 3 + moneyTransaction/100 * (generalPercentIncrementer * 3); // Incriment By Percent
		DrawPlusPoints();
	}

}
