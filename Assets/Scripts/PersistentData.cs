using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PersistentData : MonoBehaviour
{
	public static PersistentData Instance;

	//session name - tempName
	public string tempName;

	public void Awake()
	{
        Debug.Log("Awake of PermanentData: ");

        if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);


	}

	[System.Serializable]
	class SaveData
	{
		public string playerName;
		public int score;
	}//end class SaveData


	public void SaveScore(string nameSession, int scoreSession)
	{
		//saves up to 10 best scores
		//retrieve the scores, compare with them, add or not

		SaveData data = new SaveData();
		//data.playerName =
		//data.score = 

		string json = JsonUtility.ToJson(data);

		//Debug.Log("Application.persistentDataPath: " + Application.persistentDataPath);
		File.WriteAllText(Application.persistentDataPath + "/breakoutScores.json", json);
	}

	public void LoadScore()
	{
		string path = Application.persistentDataPath + "/breakoutScores.json";

		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			//player = data.playerName;
			//score = data.score;
		}
	}

}
