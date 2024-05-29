using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioClip bulletHitSound;
    public AudioClip EnemyDeathSound;
    public AudioClip bulletHitEnemy;
    public AudioClip critSoundHitEnemy;

    public AudioClip SwordHitSound;
    public AudioClip SwordSwipeSound;
    public AudioClip BackStab;
    public AudioClip CamoflageSound;
    public AudioClip MolotovSound;
    public AudioClip FlashSound;
    public AudioClip SetTrapSound;
    public AudioClip playerHitSound;
    public AudioMixer mixer;
    AudioSource audioManage;
    void Start()
    {
        Instance = this;
        audioManage = GetComponent<AudioSource>();
        audioManage.volume = 0.5f;
    }
    public void FlashingSound()
    {
        audioManage.PlayOneShot(FlashSound);

    }
    public void MolotovingSound()
    {
        audioManage.PlayOneShot(MolotovSound);
    }
    public void CamoflagingSound()
    {
        audioManage.PlayOneShot(CamoflageSound);
    }
    public void TrapSound()
    {
        audioManage.PlayOneShot(SetTrapSound);
    }
    public void HitSound()
    {
        audioManage.PlayOneShot(bulletHitSound);
    }

    public void EnemyDeadSound()
    {
        audioManage.PlayOneShot(EnemyDeathSound);
    }
    public void PlayerHurtSound()
    {
        audioManage.PlayOneShot(playerHitSound);
    }
    public void EnemyHitSound()
    {
        audioManage.PlayOneShot(bulletHitEnemy);
    }

    public void EnemyCriticalHitSound()
    {
        audioManage.PlayOneShot(critSoundHitEnemy);
    }
    public void SwipingSword()
    {
        audioManage.PlayOneShot(SwordSwipeSound);
    }
    public void BackStabSound()
    {
        audioManage.PlayOneShot(BackStab);
    }
    public void SwordHitting()
    {
        audioManage.PlayOneShot(SwordHitSound);
    }

   
}
