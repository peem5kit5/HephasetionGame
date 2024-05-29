using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneToBossFight : MonoBehaviour
{

    public void ToPigman()
    {
        StartCoroutine(ToBoss_Pigman());
    }
    public IEnumerator ToBoss_Pigman()
    {
        yield return new WaitForSeconds(1);
        SaveManager.Instance.PigmanBattle();
        SaveManager.Instance.LoadToScene();
    }
    public void ToEagle()
    {
        StartCoroutine(ToBoss_Eagle());
    }
    public IEnumerator ToBoss_Eagle()
    {
        yield return new WaitForSeconds(1);
        SaveManager.Instance.EagleBattle();
        SaveManager.Instance.LoadToScene();
    }
    public void ToSlumCutscene()
    {
        StartCoroutine(ToOutsideBar_AfterBatholoDied());
    }
    public IEnumerator ToOutsideBar_AfterBatholoDied()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Slum_AfterBatholoDeath");
    }
    public void ToSlumGamePlay()
    {
        StartCoroutine(ToGameplaySlum());
    }
    public IEnumerator ToGameplaySlum()
    {

        yield return new WaitForSeconds(0.5f);
        SaveManager.Instance.Bar_Slum();
        SaveManager.Instance.LoadToScene();

    }

    public IEnumerator ToBoss_Milady()
    {
        yield return new WaitForSeconds(1);
        SaveManager.Instance.MiladyBattle();
        SaveManager.Instance.LoadToScene();
    }
    public void ToMilady()
    {
        StartCoroutine(ToBoss_Milady());
    }
    public IEnumerator ToBoss_Nathan()
    {
        yield return new WaitForSeconds(1);
        SaveManager.Instance.NathanBattle();
        SaveManager.Instance.LoadToScene();
    }
    public void ToNathan()
    {
        StartCoroutine(ToBoss_Nathan());
    }
    public IEnumerator To_End()
    {
        yield return new WaitForSeconds(1);
        SaveManager.Instance.LoadToScene();
    }
    public void ToTheEnd()
    {
        StartCoroutine(To_End());
    }

}
