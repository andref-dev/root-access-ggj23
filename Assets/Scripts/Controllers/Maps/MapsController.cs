using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class MapsController : MonoBehaviour
{
    public Transform ContainerTransform;
    public int CurrentLayer;
    public List<GameObject> Maps = new List<GameObject>();

    public List<PinController> Map1Pins = new List<PinController>();
    public List<PinController> Map2Pins = new List<PinController>();
    public List<PinController> Map3Pins = new List<PinController>();
    public List<PinController> Map4Pins = new List<PinController>();

    public List<float> TimeToPing = new List<float>();

    public List<Vector2> NextCorrect = new List<Vector2>();

    private float _currentPingTime;

    private int _nextCorrect;

    private int _pingCounter;

    public List<Sprite> Shapes = new List<Sprite>();
    public List<Color> Colors = new List<Color>();

    public int CorrectShapeIndex;
    public int CorrectColorIndex;

    public SpriteRenderer TargetSprite;

    public StageController Stage;

    public Canvas ModalCanvas;

    public Transform Modal;

    public SpriteRenderer Heart1;
    public SpriteRenderer Heart2;
    public SpriteRenderer Heart3;

    public Sprite HeartOn;
    public Sprite HeartOff;


    public bool Finished;

    // Start is called before the first frame update
    void Start()
    {
        CurrentLayer = 0;
        foreach (var obj in Maps)
        {
            obj.SetActive(false);
        }
        ResetTarget();
        ResetNextCorrect();
        ShowMap(Vector3.zero);
        ModalCanvas.gameObject.SetActive(false);
        UpdateHearts();
    }

    public void Setup(StageController stage)
    {
        Stage = stage;
    }

    void Update()
    {
        if (Finished)
            return;
        _currentPingTime -= Time.deltaTime;
        if (_currentPingTime <= 0)
        {
            Ping();
            ResetPingTimer();
        }
    }

    private void Ping()
    {
        var pins = LayerToPins(CurrentLayer);
        var selectedPin = pins[Random.Range(0, pins.Count)];
        if (selectedPin.IsShowing)
        {
            var count = 0;
            while (count < 5 && selectedPin.IsShowing)
            {
                selectedPin = pins[Random.Range(0, pins.Count)];
                count++;
            }
        }
        if (_pingCounter == _nextCorrect)
        {
            var success = selectedPin.ShowShape(Shapes[CorrectShapeIndex], Colors[CorrectColorIndex], true);
            if (success)
                ResetNextCorrect();
            else
                _nextCorrect++;
        }
        else
        {
            var selectedShapeIndex = Random.Range(0, Shapes.Count);
            var selectedColorIndex = Random.Range(0, Colors.Count);
            while (selectedColorIndex == CorrectColorIndex && selectedShapeIndex == CorrectShapeIndex)
            {
                selectedShapeIndex = Random.Range(0, Shapes.Count);
                selectedColorIndex = Random.Range(0, Colors.Count);
            }

            selectedPin.ShowShape(Shapes[selectedShapeIndex], Colors[selectedColorIndex]);
        }
        _pingCounter++;
    }

    private void ResetPingTimer()
    {
        _currentPingTime = TimeToPing[CurrentLayer] * Random.Range(0.8f, 1.2f);
    }

    private void ResetNextCorrect()
    {
        _nextCorrect = _pingCounter + Random.Range((int)NextCorrect[CurrentLayer].x, (int)NextCorrect[CurrentLayer].y + 1);
    }

    private void ResetTarget()
    {
        var previousShape = CorrectShapeIndex;
        var previousColor = CorrectColorIndex;

        while (CorrectShapeIndex == previousShape)
            CorrectShapeIndex = Random.Range(0, Shapes.Count);
        while (CorrectColorIndex == previousColor)
            CorrectColorIndex = Random.Range(0, Colors.Count);

        var originalScale = TargetSprite.transform.localScale;
        var seq = DOTween.Sequence();
        seq.Append(TargetSprite.transform.DOScale(Vector3.zero, 0.2f));
        seq.AppendCallback(() =>
        {
            TargetSprite.sprite = Shapes[CorrectShapeIndex];
            TargetSprite.color = Colors[CorrectColorIndex];
        });
        seq.Append(TargetSprite.transform.DOScale(originalScale, 0.2f));
        seq.Play();
    }

    private void ShowMap(Vector3 pos)
    {
        _currentPingTime = 10f;
        if (CurrentLayer > 0)
        {
            foreach (var pin in LayerToPins(CurrentLayer - 1))
            {
                pin.gameObject.SetActive(false);
            }
        }

        foreach (var pin in LayerToPins(CurrentLayer))
        {
            pin.SetMap(this);
            pin.gameObject.SetActive(false);
        }

        var map = Maps[CurrentLayer];

        map.SetActive(true);
        var trans = map.transform;
        trans.localScale = Vector3.zero;
        trans.position = pos;

        var seq = DOTween.Sequence();
        seq.Append(trans.DOScale(Vector3.one, 1f).SetEase(Ease.OutCubic));
        seq.Join(trans.DOMove(ContainerTransform.position, 1f).SetEase(Ease.OutCubic));
        if (CurrentLayer > 0)
            seq.AppendCallback(() => Maps[CurrentLayer - 1].SetActive(false));

        seq.AppendCallback(() =>
        {
            foreach (var pin in LayerToPins(CurrentLayer))
            {
                pin.gameObject.SetActive(true);
            }
        });
        seq.AppendCallback(ResetPingTimer);
        seq.Play();
    }

    public void OnPinPress(Vector3 pinPosition, bool correct)
    {
        if (correct)
        {
            if (CurrentLayer < 3)
            {
                CurrentLayer++;
                ResetTarget();
                ResetNextCorrect();
                ShowMap(pinPosition);
            }
            else
            {
                // TODO: congrats
                var pins = LayerToPins(CurrentLayer);
                foreach (var pin in pins)
                {
                    pin.gameObject.SetActive(false);
                }
                ModalCanvas.gameObject.SetActive(true);
                Modal.transform.localScale = Vector3.zero;
                Modal.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InCubic);
                Finished = true;
            }
        }
        else
        {
            //TODO: Error, lose a heart
            CameraShakeController.ShakeCamera(0.5f);
            GameController.Instance.MissionHearts--;
            UpdateHearts();
            if (GameController.Instance.MissionHearts <= 0)
            {
                Stage.FailedSection();
            }
        }
    }

    private void UpdateHearts()
    {
        Heart1.sprite = GameController.Instance.MissionHearts > 0 ? HeartOn : HeartOff;
        Heart2.sprite = GameController.Instance.MissionHearts > 1 ? HeartOn : HeartOff;
        Heart3.sprite = GameController.Instance.MissionHearts > 2 ? HeartOn : HeartOff;
    }

    public void ModalPresed()
    {
        Stage.FinishedSection();
    }

    private List<PinController> LayerToPins(int layer)
    {
        switch (layer)
        {
            case 1:
                return Map2Pins;
            case 2:
                return Map3Pins;
            case 3:
                return Map4Pins;
            default:
                return Map1Pins;
        }
    }
}
