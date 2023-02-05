using UnityEngine;
using DG.Tweening;

public class PinController : MonoBehaviour
{
    public Transform ShapeTransform;
    public SpriteRenderer ShapeRenderer;

    public bool IsCorrect;
    public bool IsShowing;

    public MapsController MapsController;

    public Vector3 OriginalShapeScale;


    public void Start()
    {
        ShapeTransform.localScale = Vector3.zero;
    }

    public void SetMap(MapsController controller)
    {
        MapsController = controller;
    }

    public bool ShowShape(Sprite sprite, Color color, bool correct = false)
    {
        if (IsShowing)
            return false;

        SoundController.PlaySfx("sonar");

        IsShowing = true;
        IsCorrect = correct;

        ShapeRenderer.sprite = sprite;
        ShapeRenderer.color = color;
        ShapeTransform.localScale = Vector3.zero;

        var seq = DOTween.Sequence();
        seq.Append(ShapeTransform.DOScale(Vector3.one, 0.8f));
        seq.Append(ShapeTransform.DOScale(Vector3.one * 1.1f, 0.3f));
        seq.Join(ShapeRenderer.DOFade(0.0f, 0.3f));
        seq.AppendCallback(() =>
        {
            IsCorrect = false;
            IsShowing = false;
            ShapeTransform.localScale = Vector3.zero;
        });
        seq.Play();

        return true;
    }

    public void OnMouseDown()
    {
        if (MapsController == null)
        {
            Debug.LogError("No MapsController assigned");
            return;
        }
        MapsController.OnPinPress(transform.position, IsCorrect);
    }
}
