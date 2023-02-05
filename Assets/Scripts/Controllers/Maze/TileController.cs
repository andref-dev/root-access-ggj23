using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TileController : MonoBehaviour
{
    public Color RegularTileColor;
    public Color WallTileColor;
    public string WallConfig;

    public Transform Player;
    public Transform Key;
    public Transform Lock;

    public List<WallController> Row0 = new List<WallController>();
    public List<WallController> Row1 = new List<WallController>();
    public List<WallController> Row2 = new List<WallController>();
    public List<WallController> Row3 = new List<WallController>();
    public List<WallController> Row4 = new List<WallController>();
    public List<WallController> Row5 = new List<WallController>();
    public List<WallController> Row6 = new List<WallController>();
    public List<List<WallController>> Columns = new List<List<WallController>>();

    public CreateHandController PlayerController;
    public Vector3 OriginalPos;
    public Vector3 OriginalScale;
    public GridController Grid;

    public Transform MinPos;
    public Transform MaxPos;

    // Start is called before the first frame update
    void Start()
    {
        Columns.Clear();
        Columns.Add(Row0);
        Columns.Add(Row1);
        Columns.Add(Row2);
        Columns.Add(Row3);
        Columns.Add(Row4);
        Columns.Add(Row5);
        Columns.Add(Row6);

        OriginalPos = transform.position;
        OriginalScale = transform.localScale;
        Player.gameObject.SetActive(false);
        Key.gameObject.SetActive(false);
        Lock.gameObject.SetActive(false);
        for (var i = 0; i < Columns.Count; i++)
        {
            for (var j = 0; j < Columns[i].Count; j++)
            {
                var wall = Columns[i][j];
                wall.Collider.enabled = false;
            }
        }
    }

    public void StartTile(Vector3 pos, GridController grid)
    {
        pos.z -= 2.0f;
        Grid = grid;
        var seq = DOTween.Sequence();
        seq.Append(transform.DOMove(pos, 0.5f, false));
        seq.Join(transform.DOScale(Vector3.one, 0.5f));
        seq.AppendCallback(() =>
        {
            Player.gameObject.SetActive(true);
            Key.gameObject.SetActive(true);
            Lock.gameObject.SetActive(false);
            PlayerController.Setup(this);
            for (var i = 0; i < Columns.Count; i++)
            {
                for (var j = 0; j < Columns[i].Count; j++)
                {
                    var wall = Columns[i][j];
                    wall.Collider.enabled = wall.IsWall;
                }
            }
        });
        seq.Play();
    }

    public void GotKey()
    {
        Key.gameObject.SetActive(false);
        Lock.gameObject.SetActive(true);
    }

    public void FinishedTile()
    {
        Player.gameObject.SetActive(false);
        Lock.gameObject.SetActive(false);
        transform.DOMove(OriginalPos, 0.5f, false);
        transform.DOScale(OriginalScale, 0.5f);
        for (var i = 0; i < Columns.Count; i++)
        {
            for (var j = 0; j < Columns[i].Count; j++)
            {
                Columns[i][j].Collider.enabled = false;
            }
        }
        Grid.FinishedTile();
    }

    [ContextMenu("SetupWalls")]
    private void SetupWalls()
    {
        Columns.Clear();
        Columns.Add(Row0);
        Columns.Add(Row1);
        Columns.Add(Row2);
        Columns.Add(Row3);
        Columns.Add(Row4);
        Columns.Add(Row5);
        Columns.Add(Row6);

        var walls = WallConfig.Split(',');
        for (var i = 0; i < walls.Length; i++)
        {
            var column = i % 7;
            var row = Mathf.FloorToInt((float)i / 7.0f);
            switch (walls[i])
            {
                case "2": //player
                    Player.position = Columns[row][column].transform.position;
                    break;
                case "3": //key
                    Key.position = Columns[row][column].transform.position;
                    break;
                case "4": //lock
                    Lock.position = Columns[row][column].transform.position;
                    break;
            }

            if (walls[i] == "1")
            {
                Columns[row][column].Setup(true, WallTileColor);
            }
            else
            {
                Columns[row][column].Setup(false, RegularTileColor);
            }
        }

        Player.gameObject.SetActive(false);
        Key.gameObject.SetActive(false);
        Lock.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var pos = Player.transform.position;

        pos.x = Mathf.Clamp(pos.x, MinPos.position.x, MaxPos.position.x);
        pos.y = Mathf.Clamp(pos.y, MinPos.position.y, MaxPos.position.y);

        Player.transform.position = pos;
    }
}
