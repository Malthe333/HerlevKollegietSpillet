using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    public VideoPlayer player;
    public VideoClip MenuLoop;
    public GameObject skipTxt;
    public GameObject Menu;

    void Start()
    {
        player.Prepare();
        player.prepareCompleted += Player_prepareCompleted;
    }

    private void Player_prepareCompleted(VideoPlayer source)
    {
        player.Play();
    }

    void Update()
    {
        if (player.isPrepared)
        {
            if (player.isPlaying)
            {
                if (Input.anyKeyDown)
                {
                    if (skipTxt.activeSelf)
                    {
                        changeVideo();
                    }
                    else
                    {
                        StartCoroutine(ShowSkipText());
                    }
                }
            }
            else
            {
                changeVideo();
            }
        }
    }

    IEnumerator ShowSkipText()
    {
        skipTxt.SetActive(true);
        yield return new WaitForSeconds(3);
        skipTxt.SetActive(false);
    }

    private void changeVideo()
    {
        skipTxt.SetActive(false);
        player.clip = MenuLoop;
        player.isLooping = true;
        player.Play();
        Menu.SetActive(true);
        GameObject.Destroy(this);
    }
}
