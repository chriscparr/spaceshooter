using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get { return _instance; } }
    private static GameController _instance;

    [SerializeField] private GameObject[] _bgTiles;
    [SerializeField] private Player _player;

    private List<GameObject> _bgTileList;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _bgTileList = new List<GameObject>(_bgTiles);
        _bgTileList = _bgTileList.OrderBy(o => o.transform.position.x).ToList();
    }


    private void Update()
    {
        CheckBGTiles();
    }


    private void CheckBGTiles()
    {
        if (_player.transform.position.x - _bgTileList[0].transform.position.x >= 150f)
        {
            _bgTileList[0].transform.position = _bgTileList[2].transform.position + new Vector3(100f, 0f, 0f);
            _bgTileList = _bgTileList.OrderBy(o => o.transform.position.x).ToList();
        }
    }

}
