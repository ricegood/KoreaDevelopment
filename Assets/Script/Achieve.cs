using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Achieve : MonoBehaviour {
	private const int CONNECTION = 1;
	private const int TREE = 2;
	private const int GDP = 3;
	private const int APPRRATE = 4;

	public Button thisObject;
	public Text rewardText;

	public int index;
	public int reward;


	// complete condition
	public int type;		// 1. city~city connect / 2. tree / 3.GDP / 4.apprRate
	public City city1;
	public City city2;
	public int treeGoal;
	public int gdpGoal;
	public int apprRateGoal;

	private bool complete;
	private bool getReward;


	// Use this for initialization
	void Start () {
		complete = (PlayerPrefs.GetString ("achievement" + index) == "True");
		getReward = (PlayerPrefs.GetString ("achievementGetReward" + index) == "True");
	}
	
	// Update is called once per frame
	void Update () {
		if (!complete) {
			switch (type) {
			case CONNECTION:
				if (Map.isLinked (city1, city2)) {
					complete = true;
					PlayerPrefs.SetString("achievement"+index, complete.ToString());
				}
				break;
			case TREE:
				if (City.getTreeNum() >= treeGoal) {
					complete = true;
					PlayerPrefs.SetString("achievement"+index, complete.ToString());
				}
				break;
			case GDP:
				if (Country.getAvgGDP() >= gdpGoal) {
					complete = true;
					PlayerPrefs.SetString("achievement"+index, complete.ToString());
				}
				break;
			case APPRRATE:
				if (Country.getAvgApprRate() >= apprRateGoal) {
					complete = true;
					PlayerPrefs.SetString("achievement"+index, complete.ToString());
				}
				break;
			}
		}
		else {
			thisObject.interactable = true;
		}
	}

	public void giveReward(){
		if(getReward){
			//already get reward
			rewardText.text = "You have already claimed your reward.";
		}
		else{
			getReward = true;
			PlayerPrefs.SetString ("achievementGetReward" + index, getReward.ToString());
			rewardText.text = Util.printIntValue(reward) +" has been added to the budget as a reward!" ;
			Country.setMoney (Country.getMoney () + reward);
		}
	}
}
