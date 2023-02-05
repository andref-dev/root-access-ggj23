using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicPlayerConrtoller : MonoBehaviour
{
    public SpriteRenderer ButtonRenderer;
    public Sprite PlayImage;
    public Sprite StopImage;

    public SpriteButton button;

    public TextMeshPro Title;
    public TextMeshPro Duration;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SoundController.Instance == null)
            return;

        if (!SoundController.Instance.BmgSource.isPlaying)
        {
            return;
        }

        ButtonRenderer.sprite = StopImage;
        Title.text = SoundController.Instance.ActualBmg;

        var currentTime = SoundController.Instance.BmgSource.time;
        var maxTime = SoundController.Instance.BmgSource.clip.length;

        Duration.text = floatToTime(currentTime) + "/" + floatToTime(maxTime);

    }

    private string floatToTime(float time)
    {
        float secondsRemainder = Mathf.Floor((time % 60) * 100) / 100.0f;
        int minutes = ((int)(time / 60)) % 60;
        return System.String.Format("{0:00}:{1:00}", minutes, secondsRemainder);
    }

    public void PlayButton()
    {
        if (SoundController.Instance == null)
            return;

        if (!SoundController.Instance.BmgSource.isPlaying)
        {
            return;
        }

        SoundController.Instance.BmgSource.Stop();
        SoundController.Instance.ActualBmg = "";
        ButtonRenderer.sprite = PlayImage;
        Duration.text = "--/--";
        Title.text = "Music Player";
    }
}
