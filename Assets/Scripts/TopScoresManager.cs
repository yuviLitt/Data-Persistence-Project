using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TopScoresManager : MonoBehaviour
{
	void Start()
	{

        //check if records exist - exception maybe NullReferenceException
        if (PersistentData.Instance.topScores != null) {
			StartCoroutine(ShowScores());
		}

    }

	
    IEnumerator ShowScores() {

        TextMeshProUGUI scoreTextAux;
		Transform child;

        //for (int i = 0; i < PersistentData.Instance.topScores.Count; i++) {
        for (int i = PersistentData.Instance.topScores.Count-1; i >= 0; i--) { 
            
            yield return new WaitForSeconds(1);

            //Get corresponding TMPro child of panel
            //0 is the title
            child = transform.GetChild(i + 1);
			//Debug.Log("i: " + i + "/ child: " + child.name);

			scoreTextAux = child.gameObject.GetComponent<TextMeshProUGUI>();
			scoreTextAux.text = i + " : " + PersistentData.Instance.topScores[i].playerName +
				" : " + PersistentData.Instance.topScores[i].score;

		}
	   
	}

}
