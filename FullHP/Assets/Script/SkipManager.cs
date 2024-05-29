using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipManager : MonoBehaviour
{
    public enum Cutscene
    {
        IntroCutscene,
        PigmanCutscene,
        EagleCutscene,
        MiladyCutscene,
        NathanCutscene,
    }

    public Cutscene cutsceneSkip;

    public void CheckCutscene()
    {
        switch (cutsceneSkip)
        {
            case Cutscene.IntroCutscene:
                SaveManager.Instance.NewGame();
                break;
            case Cutscene.PigmanCutscene:
                SaveManager.Instance.PigmanBattle();
                SaveManager.Instance.LoadToScene();
                break;
            case Cutscene.EagleCutscene:
                SaveManager.Instance.EagleBattle();
                SaveManager.Instance.LoadToScene();
                break;
            case Cutscene.MiladyCutscene:
                SaveManager.Instance.MiladyBattle();
                SaveManager.Instance.LoadToScene();
                break;
            case Cutscene.NathanCutscene:
                SaveManager.Instance.NathanBattle();
                SaveManager.Instance.LoadToScene();
                break;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
