using UnityEngine;
using System.Collections;

public class PlayAdventureModeMusic : MonoBehaviour
{
    public AudioClip BackgroundMusic;

    void Start()
    {
        AudioManager.Instance.PlayAudio(BackgroundMusic);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
