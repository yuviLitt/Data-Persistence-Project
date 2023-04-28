using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour
{
	public static PersistentData Instance;

	//session name/game points
	public string tempName;
	public int tempPoints;

	public string bestPlayer;
	public string bestScore;

	public List<SaveData> topScores = new List<SaveData>();

    //settings game
    //linecount bricks
    //color bricks - sets of colors
    //sounds?? on/off
    public Color[] setOfColors;


    public void Awake()
	{
        tempName = "";
		tempPoints = 0;
		bestPlayer = "";
		bestScore = "0";
		setOfColors = new Color[4];

		//default set of colors
		DefaultColors();

        //Debug.Log("Awake of PersistentData: ");

        if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);

		//Load score file - YEP - Test LoadScore
		LoadScore();

	}

	[System.Serializable]
	public class SaveData
	{
		public string playerName;
		public int score;
	}//end class SaveData


	public void SaveScore()
	{
		//saves up to 10 best scores
		//retrieve the scores (topScores), compare with them, add or not
		//compare to the best scores
		List<SaveData> auxTops = new List<SaveData>(topScores);
		bool recordInserted = false;

		//topScores.Count = 10
		//foreach (var line in topScores)
		for (int i = 0; i < auxTops.Count; i++)
		{

            if (tempPoints > auxTops[i].score) {

				//insert new score-player before the one checked 
				CreateRecord(i, 0); //insert
				recordInserted = true;
				break;
			}

		}

		//delete last score if more than 10 records exist, i = 10
		//or add record if less than 10 records exist
		if (topScores.Count > 10)
		{
			topScores.Remove(topScores[10]);
		}
		else if (topScores.Count < 10 && !recordInserted)
		{
			CreateRecord(-1, 1); //add to the end - index irrelevant
		}

		string json = "";
       
		for(int i = 0; i < topScores.Count; i++) {

			json += JsonUtility.ToJson(topScores[i]);

			//dont add line break in last line
			if (i < topScores.Count-1) {
                json += "\n";
            }

		}
		
        //Debug.Log("json to write: " + json);

		//Debug.Log("Application.persistentDataPath: " + Application.persistentDataPath);
		File.WriteAllText(Application.persistentDataPath + "/breakoutScores.json", json);

		/*
		//TO SEE
		foreach (var line in topScores) {
			Debug.Log("topScoreLine: " + line.playerName + ": " + line.score);
		}
		*/

		//update best markers - in main-startmenu
		bestPlayer = topScores[0].playerName;
		bestScore = topScores[0].score.ToString();

    }

	//type: 0 - Insert // 1 - Add
	private void CreateRecord(int index, int type) {

		SaveData newLine = new SaveData();
		newLine.playerName = tempName;
		newLine.score = tempPoints;

		if (type == 0)
		{
			topScores.Insert(index, newLine);
		}
		else {
			topScores.Add(newLine);
		}

	}

	private void LoadScore()
	{
		string path = Application.persistentDataPath + "/breakoutScores.json";

		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			string[] lines = json.Split("\r\n".ToCharArray());

			//Debug.Log("lines count when loading: " + lines.Length);

			for (int i = 0; i < lines.Length; i++) {

				//Debug.Log(i + " " + lines[i] );
				SaveData dataLine = JsonUtility.FromJson<SaveData>(lines[i]);
				topScores.Add(dataLine);

			}

			bestPlayer = topScores[0].playerName;
			bestScore = topScores[0].score.ToString();

		}
	}

	private void DefaultColors() {
        setOfColors[0] = Color.green;
        setOfColors[1] = Color.yellow;
        setOfColors[2] = Color.blue;
        setOfColors[3] = Color.red;
    }

}
