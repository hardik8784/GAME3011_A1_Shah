/*
 * Full Name: Hardik Dipakbhai Shah
 * Student ID : 101249099
 * Date Modified : December 30,2022
 * File : GameManager.cs
 * Description : This is GameManager Script
 * Revision History : v0.1 > 
 *              
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject ModeToggleButton;

    public TMP_Text ModeToggleButton_Text;

    // Start is called before the first frame update
    void Start()
    {
        ModeToggleButton.GetComponent<Button>().onClick.AddListener(OnModeToggleButtonClicked);

        ModeToggleButton_Text = ModeToggleButton.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnModeToggleButtonClicked()
    {
        if (MiniGame.Instance.Toogle_mode == TOOGLE_MODE.EXTRACT_MODE)
        {
            MiniGame.Instance.Toogle_mode = TOOGLE_MODE.SCAN_MODE;
            ModeToggleButton_Text.text = "Extract Mode";
            MiniGame.Instance.MessageText.text = "You are in scan mode";
            //print("Scan Mode");
        }
        else
        {
            MiniGame.Instance.Toogle_mode = TOOGLE_MODE.EXTRACT_MODE;
            ModeToggleButton_Text.text = "Scan Mode";
            MiniGame.Instance.MessageText.text = "You are in extract mode";
        }
       
    }
}

public enum TOOGLE_MODE
{
    EXTRACT_MODE,
    SCAN_MODE
}

public enum LIST_OF_RESOURCE
{
    B_T_01,
    BAG,
    BELTS,
    BOOK,
    BOOTS,
    BRACERS,
    CLOAKS,
    GLOVES,
    NECKLACE,
    PANTS,
    RINGS,
    SCROLL

}
