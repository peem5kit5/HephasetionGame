using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public static PlayerDeath Instance { get; private set; }
    public Animator FadeBlackAnim;
    public enum TypeDeath
    {
        Default,
        Pigman,
        Eagle,
        Milady,
        Nathan
    }

    private void Start()
    {
        Instance = this;
    }
    public void Death()
    {
        CheckBoss();
    }

    void CheckBoss()
    {
        switch (type)
        {
            default:
            case TypeDeath.Default:
                StartCoroutine(DeathMoment());
                break;
            case TypeDeath.Pigman:
                StartCoroutine(DeathPigman());
                break;
            case TypeDeath.Eagle:
                StartCoroutine(DeathEagle());
                break;
            case TypeDeath.Milady:
                StartCoroutine(DeathMilady());
                break;


        }
    }
    public TypeDeath type;

    IEnumerator DeathPigman()
    {
        yield return new WaitForSeconds(1);
        FadeBlackAnim.SetTrigger("ChangeGo");
        SaveManager.Instance.OutSide_Pigman();
        yield return new WaitForSeconds(1);
        SaveManager.Instance.LoadToScene();
    }
    IEnumerator DeathMilady()
    {
        yield return new WaitForSeconds(1);
        FadeBlackAnim.SetTrigger("ChangeGo");
        yield return new WaitForSeconds(1);
        SaveManager.Instance.Outside_Milady();
        SaveManager.Instance.LoadToScene();
    }
    IEnumerator DeathEagle()
    {
        yield return new WaitForSeconds(1);
        FadeBlackAnim.SetTrigger("ChangeGo");
        yield return new WaitForSeconds(1);
        SaveManager.Instance.Outside_Eagle();
        SaveManager.Instance.LoadToScene();
    }

    IEnumerator DeathMoment()
    {
        yield return new WaitForSeconds(0.5f);
        FadeBlackAnim.SetTrigger("ChangeGo");
        yield return new WaitForSeconds(1);
        SaveManager.Instance.LoadToScene();
    }
}
