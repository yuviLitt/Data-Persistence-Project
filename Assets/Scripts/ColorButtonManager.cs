using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonManager : MonoBehaviour
{
    private Button colorButton;

    public Color[] chosenSet;

    // Start is called before the first frame update
    void Start()
    {
        colorButton = gameObject.GetComponent<Button>();
        colorButton.onClick.AddListener(ChangeBrickColors);
    }

    private void ChangeBrickColors() {

        //Debug.Log("button set of colors clicked: " + gameObject.name);
        PersistentData.Instance.setOfColors = chosenSet;

    }

}
