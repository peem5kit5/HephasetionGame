using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEnd : MonoBehaviour
{
    VideoPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.Prepare();
        player.loopPointReached += OnVideoEnd;
    }

    // Update is called once per frame
    
    private void OnVideoEnd(VideoPlayer vp)
    {
        // Load the next scene
        SaveManager.Instance.NewGame();
    }
}
