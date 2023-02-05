using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : MonoBehaviour
{
    public Animator ThunderAnimator;

    public float ThunderTime;
    private float _currentThunderTime;

    // Start is called before the first frame update
    void Start()
    {
        _currentThunderTime = ThunderTime * Random.Range(0.8f, 1.3f);
    }

    // Update is called once per frame
    void Update()
    {
        _currentThunderTime -= Time.deltaTime;
        if (_currentThunderTime <= 0)
        {
            ThunderAnimator.SetTrigger("Thunder");
            _currentThunderTime = ThunderTime * Random.Range(0.8f, 1.3f);
        }
    }

    public void PlayThunder()
    {
        SoundController.PlaySfx("thunder");
    }
}
