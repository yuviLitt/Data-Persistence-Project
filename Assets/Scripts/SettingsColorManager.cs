using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsColorManager : MonoBehaviour
{

	public string nameSet;
	public Color[] chosenSet;
	public Toggle toggleSet;

	// Start is called before the first frame update
	void Start()
	{
       
		 //get the one active, and activate the corresponding toggle
        if (nameSet.Equals(PersistentData.Instance.nameSetColors))
        {
            toggleSet.isOn = true;
        }
		


        //fill the fields with the parameters
        for (int i = 0; i < 5; i++) {
			//Debug.Log(i + " : child: " + transform.GetChild(i).name);
			ConfigValuesChildren(i, transform.GetChild(i));
		}

	}

	private void ConfigValuesChildren(int index, Transform child) {

		if (index == 0) {// name of the set

			TextMeshProUGUI nameSetText = child.gameObject.GetComponent<TextMeshProUGUI>();
			nameSetText.text = nameSet;

		}
		else{

			Image imageAux = child.gameObject.GetComponent<Image>();
			imageAux.color = chosenSet[index-1];

		}

	}


	//for toggle - when changes to ON
	public void SetChosenColors() {

		if (toggleSet.isOn) {

			//Debug.Log("which toggle is on?: " + toggleSet.transform.parent.name);

			PersistentData.Instance.nameSetColors = nameSet;
			PersistentData.Instance.setOfColors = chosenSet;
			PersistentData.Instance.SaveColors();
		}

	}

}
