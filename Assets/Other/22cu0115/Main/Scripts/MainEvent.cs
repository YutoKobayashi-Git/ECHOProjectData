using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MainEvent : MonoBehaviour
{
    public PlayableDirector director;

    private void OnEnable()
    {
         director.stopped += OnPlayableDirectorStopped;
    }

    private void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            Debug.Log("PlayableDirector named " + aDirector.name + " is now stopped.");
            SceneManager.LoadScene("Main");
        }
    }

    private void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }
}
