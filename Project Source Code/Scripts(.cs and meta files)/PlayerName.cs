using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerName : MonoBehaviour {

     [SerializeField] private TMP_Text inputText;
     // Saves current name to the computer
     public void SetName()
     {
         PlayerPrefs.SetString("CurrentName", inputText.text);
     }
}
