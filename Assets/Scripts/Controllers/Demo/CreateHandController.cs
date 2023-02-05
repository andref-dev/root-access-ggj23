using UnityEngine;
using DG.Tweening;

public class CreateHandController : MonoBehaviour
{
    public float Speed;

    public Sprite OpenEye;
    public Sprite CloseEye;

    public SpriteRenderer SpriteRenderer;
    public bool Opened;
    public float OpenEyeTime;
    public float CloseEyeTime;

    private float _currentEyeTime;

    public Vector3 Squatch = new Vector3(1.2f, 0.8f, 1.0f);
    public Vector3 Stretch = new Vector3(0.8f, 1.2f, 1.0f);

    public Rigidbody2D Body;

    public TileController Tile;

    public void Setup(TileController tile)
    {
        Tile = tile;
    }

    void Start()
    {
        _currentEyeTime = OpenEyeTime * Random.Range(0.7f, 1.3f);
        Opened = true;

        var scale = transform.localScale;
        var seq = DOTween.Sequence();
        seq.Append(transform.DOScale(new Vector3(scale.x * Squatch.x, scale.y * Squatch.y, 1), 0.8f).SetEase(Ease.OutCubic));
        seq.Append(transform.DOScale(scale, 0.3f).SetEase(Ease.InCubic));
        seq.Append(transform.DOScale(new Vector3(scale.x * Stretch.x, scale.y * Stretch.y, 1), 0.8f).SetEase(Ease.OutCubic));
        seq.Append(transform.DOScale(scale, 0.3f).SetEase(Ease.InCubic));
        seq.SetLoops(-1);
        seq.Play();
    }

    void Update()
    {
        _currentEyeTime -= Time.deltaTime;
        if (_currentEyeTime < 0)
        {
            Opened = !Opened;

            _currentEyeTime = Opened ? OpenEyeTime : CloseEyeTime;
            _currentEyeTime = _currentEyeTime * Random.Range(0.7f, 1.4f);
            SpriteRenderer.sprite = Opened ? OpenEye : CloseEye;
        }
    }

    void FixedUpdate()
    {
        var hor = InputController.Horizontal;
        var ver = InputController.Vertical;

        var dir = new Vector3(hor, ver, 0);
        dir.Normalize();

        Body.velocity = dir * Speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (Tile == null)
            return;

        if (col.gameObject == Tile.Key.gameObject)
        {
            Tile.GotKey();
        }
        else if (col.gameObject == Tile.Lock.gameObject)
        {
            Tile.FinishedTile();
        }
    }
}
