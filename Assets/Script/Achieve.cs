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
	public float gdpGoalRate;
	public int apprRateGoal;

	private int prevGdpSum;

	private bool complete;
	private bool getReward;


	// Use this for initialization
	void Start () {
		load ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!complete) {
			checkComplete ();
		}
		else {
			thisObject.interactable = true;
		}
	}

	public void checkComplete(){
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
			if (Country.getSumGDP() >= prevGdpSum*gdpGoalRate) {
				complete = true;
				PlayerPrefs.SetString(Character.getOrder() + "achievement"+index, complete.ToString());
			}
			break;
		case APPRRATE:
			if (Country.getAvgApprRate() >= apprRateGoal) {
				complete = true;
				PlayerPrefs.SetString(Character.getOrder() + "achievement"+index, complete.ToString());
			}
			break;
		}
	}

	public void giveReward(){
		if(getReward){
			//already get reward
			rewardText.text = "You have already claimed your reward.";
		}
		else{
			getReward = true;
			if (type == 3 || type == 4) {
				PlayerPrefs.SetString (Character.getOrder() + "achievementGetReward" + index, getReward.ToString ());
			} else {
				PlayerPrefs.SetString ("achievementGetReward" + index, getReward.ToString ());
			}
			rewardText.text = Util.printIntValue(reward) +" has been added to the budget as a reward!" ;
			Country.setMoney (Country.getMoney () + reward);
		}
	}

	public void load(){
		if (Character.getOrder () == 1) {
			prevGdpSum = 704000;
		} else {
			prevGdpSum = PlayerPrefs.GetInt (Character.getOrder() - 1 + "GDP");
		}

		if (type == 3 || type == 4) {
			// GDP , apprRate
			complete = (PlayerPrefs.GetString (Character.getOrder() + "achievement" + index) == "True");
			getReward = (PlayerPrefs.GetString (Character.getOrder() + "achievementGetReward" + index) == "True");
		} else {
			complete = (PlayerPrefs.GetString ("achievement" + index) == "True");
			getReward = (PlayerPrefs.GetString ("achievementGetReward" + index) == "True");
		}
	}

	public bool getComplete(){
		return complete;
	}

	public bool getGetReward(){
		return getReward;
	}
}
