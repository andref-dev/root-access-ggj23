using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridController : MonoBehaviour
{
    public int CurrentTile;

    public List<TileController> Tiles = new List<TileController>();

    public Transform TilesContainer;

    public StageController Stage;

    // Start is called before the first frame update
    void Start()
    {
        var seq = DOTween.Sequence();
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() =>
        {
            Tiles[CurrentTile].StartTile(TilesContainer.position, this);
        });
        seq.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(StageController stage)
    {
        Stage = stage;
    }

    public void FinishedTile()
    {
        CurrentTile++;

        if (CurrentTile >= Tiles.Count)
        {
            //TODO: Congrats
            Stage.FinishedSection();
            Debug.Log("Finished stage");
            return;
        }

        var seq = DOTween.Sequence();
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() =>
        {
            Tiles[CurrentTile].StartTile(TilesContainer.position, this);
        });
        seq.Play();
    }
}
