using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
		//Debug.Log("Start of StartMenuManager: ");
		if (PersistentData.Instance.bestPlayer != "")
		{
			bestScore.text = "Best Score: " + PersistentData.Instance.bestPlayer + ": " + PersistentData.Instance.bestScore;
		}
		else {
			bestScore.text = "Best Score: ";
        }

		
        if (!PersistentData.Instance.tempName.Equals(""))
        {
			playerInputField.text = PersistentData.Instance.tempName;
        }
    }

	//for game
	public void EndName() {

        //Debug.Log("OnEndEdit - when do you execute?");
		//when INTRO, changes and change of scene
		
		//if no name, put something generic
		if (playerInputField.text == null || playerInputField.text.Equals("")
			|| playerInputField.text.Contains("Enter your")){
			playerName = "JDoe";
            playerInputField.text = playerName;
        }
		else {
            playerName = playerInputField.text;
        }

        Debug.Log("player name changed: " + playerName);
        PersistentData.Instance.tempName = playerName;
		
    }

	public void ShowScores() {

        //Go to scene best Scores
        SceneManager.LoadScene(2);

    }

    public void ShowSettings()
    {
        //Go to scene settings
        SceneManager.LoadScene(3);

    }


    public void PlayGame()
	{
		EndName(); //in case it hasnt changed
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
