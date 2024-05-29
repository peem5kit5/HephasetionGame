using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SkillPopUp : MonoBehaviour
{
    public GameObject popUp;
    public string skillName;
    public string skillDescription;

    public TextMeshProUGUI skillText;
    public TextMeshProUGUI skillDesText;
    

    public void ShowPopUp()
    {
        skillText.text = skillName;
        skillDesText.text = skillDescription;
        popUp.SetActive(true);
    }
  

    // Update is called once per frame
   

    public void HidePopUp()
    {
        popUp.SetActive(false);
    }
}

