using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;


public class StartMenuManager : MonoBehaviour
{

	//public TextMeshProUGUI highScore;
	public TMP_InputField playerInputField;

	private string playerName = "";

	// Start is called before the first frame update
	void Start()
	{
        //if no name, put "Player_1"
        //playerName = "Player_Dummy";
    }

	public void ChangeName() {

		playerName = playerInputField.text;

		//if no name, put "Player_1"
		if (playerName == null || playerName.Equals("")
			|| playerName.Contains("Enter your")){
			playerName = "Player_Dummy";
		}

        Debug.Log("player name changed: " + playerName);
        PersistentData.Instance.tempName = playerName;
		//Debug.Log("player name changed: " + playerName);

	}

	public void PlayGame()
	{
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
