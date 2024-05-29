using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EXPBar : MonoBehaviour
{
    public Slider xpBar;
    private LevelSystem level;
    void Start()
    {
        level = FindObjectOfType<LevelSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        xpBar.maxValue = level.expToNextLevel;
        xpBar.value = level.exp;
    }
}
