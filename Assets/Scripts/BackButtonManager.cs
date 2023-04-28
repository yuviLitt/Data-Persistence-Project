using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButtonManager : MonoBehaviour
{

	private Button backButton;

	private void Start()
	{
		backButton = gameObject.GetComponent<Button>();
		backButton.onClick.AddListener(GoBackToMenu);
	}


	public void GoBackToMenu()
	{
		SceneManager.LoadScene(0);
	}
}
