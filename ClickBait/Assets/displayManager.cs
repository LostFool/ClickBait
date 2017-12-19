using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayManager : MonoBehaviour {
    

    public GameObject foodText, woodText, stoneText, metalText, peonText, researchText, blockText, toolText, weaponText, clothesText, homeText;

    public GameObject resourceMan_GO;

    resourceManager resourceMan;

    Text inputText;

    private void Awake()
    {
        resourceMan = resourceMan_GO.GetComponent<resourceManager>();
    }

    void Update()
    {
        updateDisplay();
    }

    void updateDisplay()
    {

        //Updates the Resource Display with current values
        inputText = foodText.GetComponent<Text>();
        inputText.text = resourceMan.foodCount.ToString();

        inputText = woodText.GetComponent<Text>();
        inputText.text = resourceMan.woodCount.ToString();

        inputText = stoneText.GetComponent<Text>();
        inputText.text = resourceMan.stoneCount.ToString();

        inputText = metalText.GetComponent<Text>();
        inputText.text = resourceMan.metalCount.ToString();

    }
}
