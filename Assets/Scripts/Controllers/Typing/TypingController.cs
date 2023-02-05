using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

[System.Serializable]
public enum WordType
{
    ThreeLetters,
    FourLetters,
    SixLetters,
    TenLetter
}

[System.Serializable]
public class TypeWord
{
    public WordType Type;
    public float TimeToFall = 3.0f;
}

public class TypingController : MonoBehaviour
{
    public WordsData Data;

    private int _points;
    public int Points
    {
        set
        {
            _points = value;
            FrontBar.fillAmount = Mathf.InverseLerp(0, RequiredPoins, _points);
            FrontBar.transform.DOKill(false);
            FrontBar.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
        }
        get
        {
            return _points;
        }
    }
    public int RequiredPoins;
    public List<TypeWord> Words = new List<TypeWord>();
    public int CurrentWordSpawn;
    public Vector2 SpawnWaitTime = new Vector2(3.0f, 1.0f);
    private float _currentSpawnTime;
    public TMP_InputField InputField;
    public Image FrontBar;
    public GameObject WordPrefab;
    public List<WordController> WordControllers = new List<WordController>();

    public Transform WordsParent;
    public Transform SpawnMin;
    public Transform SpawnMax;
    public Transform Target;

    public StageController Stage;

    public Canvas Canvas;

    public Image Heart1;
    public Image Heart2;
    public Image Heart3;
    public Sprite HeartOn;
    public Sprite HeartOff;

    // Start is called before the first frame update
    void Start()
    {
        Points = 0;
        _currentSpawnTime = 0.01f;
        InputField.Select();
        InputField.ActivateInputField();
        Canvas.worldCamera = Camera.main;
        UpdateHearts();
    }

    public void Setup(StageController stage)
    {
        Stage = stage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Points >= RequiredPoins)
            return;

        if (InputController.Return)
        {
            ProcessInput();
        }
        _currentSpawnTime -= Time.deltaTime;
        if (_currentSpawnTime >= 0)
            return;

        CurrentWordSpawn++;
        if (CurrentWordSpawn >= Words.Count)
            CurrentWordSpawn -= 8;
        ResetSpawnTime();
        Spawn();
    }

    private void ProcessInput()
    {
        var value = InputField.text.ToLower();
        InputField.text = "";
        InputField.Select();
        InputField.ActivateInputField();

        WordController wordController = null;
        foreach (var word in WordControllers)
        {
            if (word.Word == value)
            {
                wordController = word;
                break;
            }
        }

        if (wordController == null)
        {
            CameraShakeController.ShakeCamera(0.5f);
            GameController.Instance.MissionHearts--;
            UpdateHearts();
            if (GameController.Instance.MissionHearts <= 0)
            {
                Stage.FailedSection();
            }
            return;
        }

        wordController.transform.DOKill(false);
        wordController.Label.enabled = false;
        SoundController.PlaySfx("correct");
        wordController.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f).OnComplete(() =>
        {
            Points += wordController.Word.Length;
            WordControllers.Remove(wordController);
            Destroy(wordController.gameObject);
            CheckVictory();
        });
    }

    private void UpdateHearts()
    {
        Heart1.sprite = GameController.Instance.MissionHearts > 0 ? HeartOn : HeartOff;
        Heart2.sprite = GameController.Instance.MissionHearts > 1 ? HeartOn : HeartOff;
        Heart3.sprite = GameController.Instance.MissionHearts > 2 ? HeartOn : HeartOff;
    }

    private void CheckVictory()
    {

        if (Points < RequiredPoins)
            return;

        Stage.FinishedSection();
        foreach (var word in WordControllers)
        {
            word.transform.DOKill(false);
            word.Label.enabled = false;
        }
    }

    private void Spawn()
    {
        var word = Words[CurrentWordSpawn];
        var pos = new Vector3(
            Random.Range(SpawnMin.position.x, SpawnMax.position.x),
            Random.Range(SpawnMin.position.y, SpawnMax.position.y),
            transform.position.z
        );
        var obj = Instantiate(WordPrefab, pos, Quaternion.identity, WordsParent);

        var wordController = obj.GetComponent<WordController>();
        if (wordController == null)
        {
            Debug.LogError("Instantiated prefab has no 'WordController' component on it");
            return;
        }
        var text = GetTextForWordType(word.Type).ToLower();
        wordController.Setup(text);

        var target = pos;
        target.y = Target.position.y;

        obj.transform.DOMove(target, word.TimeToFall)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
        {
            WordControllers.Remove(wordController);
            CameraShakeController.ShakeCamera(0.5f);
            GameController.Instance.MissionHearts--;
            if (GameController.Instance.MissionHearts <= 0)
            {
                Stage.FailedSection();
            }
            Destroy(obj);
        });

        WordControllers.Add(wordController);

        InputField.Select();
        InputField.ActivateInputField();
    }

    private void ResetSpawnTime()
    {
        var percent = Mathf.InverseLerp(0, Words.Count - 1, CurrentWordSpawn);
        _currentSpawnTime = Mathf.Lerp(SpawnWaitTime.x, SpawnWaitTime.y, percent);
        _currentSpawnTime *= Random.Range(0.6f, 1.4f);

    }

    private string GetTextForWordType(WordType type)
    {
        switch (type)
        {
            case WordType.FourLetters:
                return Data.FourLetters[Random.Range(0, Data.FourLetters.Count)];
            case WordType.SixLetters:
                return Data.SixLetters[Random.Range(0, Data.SixLetters.Count)];
            case WordType.TenLetter:
                return Data.TenLetters[Random.Range(0, Data.TenLetters.Count)];
            default:
                return Data.ThreeLetters[Random.Range(0, Data.ThreeLetters.Count)];
        }
    }
}
