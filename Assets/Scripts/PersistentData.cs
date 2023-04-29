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
	//color bricks - sets of colors
	public Color[] setOfColors;
	public string nameSetColors;


	public void Awake()
	{
        //Debug.Log("Awake of PersistentData");

        tempName = "";
		tempPoints = 0;
		bestPlayer = "";
		bestScore = "0";
		setOfColors = new Color[4];

		//default set of colors
		LoadColorSet();

		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);

		//Load score file
		LoadScore();

	}


	//TOP SCORES
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

		for (int i = 0; i < auxTops.Count; i++)
		{

			if (tempPoints > auxTops[i].score)
			{

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

		for (int i = 0; i < topScores.Count; i++)
		{

			json += JsonUtility.ToJson(topScores[i]);

			//dont add line break in last line
			if (i < topScores.Count - 1)
			{
				json += "\n";
			}

		}

		//Debug.Log("json to write: " + json);
		//Debug.Log("Application.persistentDataPath: " + Application.persistentDataPath);
		File.WriteAllText(Application.persistentDataPath + "/breakoutScores.json", json);

		//update best markers - in main-startmenu
		bestPlayer = topScores[0].playerName;
		bestScore = topScores[0].score.ToString();

	}

	//type: 0 - Insert // 1 - Add
	private void CreateRecord(int index, int type)
	{

		SaveData newLine = new SaveData();
		newLine.playerName = tempName;
		newLine.score = tempPoints;

		if (type == 0)
		{
			topScores.Insert(index, newLine);
		}
		else
		{
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

			for (int i = 0; i < lines.Length; i++)
			{

				//Debug.Log(i + " " + lines[i] );
				SaveData dataLine = JsonUtility.FromJson<SaveData>(lines[i]);
				topScores.Add(dataLine);

			}

			bestPlayer = topScores[0].playerName;
			bestScore = topScores[0].score.ToString();

		}
	}

	//SETTINGS FOR COLORS
	[System.Serializable]
	public class SetColors
	{
		public string name;
		public Color[] colors;

	}//end class SetColors


	// load of the color set chosen
	private void LoadColorSet() {

		string path = Application.persistentDataPath + "/breakoutSettings.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SetColors chosen = JsonUtility.FromJson<SetColors>(json);
			Debug.Log("chosen set in file: " + chosen.name);

			//get the colors
			nameSetColors = chosen.name;
            setOfColors[0] = chosen.colors[0];
            setOfColors[1] = chosen.colors[1];
            setOfColors[2] = chosen.colors[2];
            setOfColors[3] = chosen.colors[3];

        }

		else{
			//save and load the default "Originals"
			SaveDefaultSetColors();
        }


	}

    //create .json with default values for colors
    private void SaveDefaultSetColors()
    {
        string path = Application.persistentDataPath + "/breakoutSettings.json";
        string json = "";
        SetColors newSet = new SetColors();

        //default: set1
        setOfColors[0] = Color.green;
        setOfColors[1] = Color.yellow;
        setOfColors[2] = Color.blue;
        setOfColors[3] = Color.red;

        newSet.name = "Originals";
        newSet.colors = setOfColors;

        json = JsonUtility.ToJson(newSet);
        File.WriteAllText(path, json);

        Debug.Log("Originals saved and loaded");
    }

    public void SaveColors()
    {
        //colors
        string path = Application.persistentDataPath + "/breakoutSettings.json";
        String json = "";
        SetColors newSet = new SetColors();

        newSet.name = nameSetColors;
        newSet.colors = setOfColors;

        json = JsonUtility.ToJson(newSet);
        File.WriteAllText(path, json);

    }

}
