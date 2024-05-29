using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimatorPlayerForCutscene : MonoBehaviour
{
    public PlayableDirector playerDirector;
    public PlayableDirector PigmanDirector;

    void Start()
    {
        if(playerDirector != null)
        {
            playerDirector.Play();
        }
        if(PigmanDirector != null)
        {
            PigmanDirector.Play();
        }
        // Play the player's timeline
        // Play the enemy's timeline
    }
}
