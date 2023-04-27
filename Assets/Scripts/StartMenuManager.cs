using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;


public class StartMenuManager : MonoBehaviour
{

	public TextMeshProUGUI bestScore;
	public TMP_InputField playerInputField;


	private string playerName = "";

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log("Start of StartMenuManager: ");
		bestScore.text = "Best Score: " + PersistentData.Instance.bestPlayer + ": " + PersistentData.Instance.bestScore;
        if (!PersistentData.Instance.tempName.Equals(""))
        {
			playerInputField.text = PersistentData.Instance.tempName;
        }
    }

	public void SetPlayerName() {

		playerName = playerInputField.text;

		//if no name, put "Player_1"
		if (playerName == null || playerName.Equals("")
			|| playerName.Contains("Enter your")){
			playerName = "Player_Dummy";
		}


		Debug.Log("player name changed: " + playerName);
		PersistentData.Instance.tempName = playerName;



	}

	public void ShowScores() {

		//Go to scene best Scores

		/*
		//This in scene best scores
		//string name = "";
		//string score = "";

		//PersistentData.Instance.lines.Length = 10
		for (int i = 0; i < 10; i++) {

			//name = PersistentData.Instance.GetName(i);
			//score = PersistentData.Instance.GetScore(i).ToString();

			//Fill element in table/text element?

		}
		*/

	}


	public void PlayGame()
	{
		SetPlayerName();
		SceneManager.LoadScene(1);
	}

	public void Exit()
	{
		//Debug.Log("saves last used name when Exit?");

#if UNITY_EDITOR
		EditorApplication.ExitPlaymode();
#else
		Application.Quit(); // original code to quit Unity player
#endif
	}


}
