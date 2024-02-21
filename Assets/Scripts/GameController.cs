using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get { return _instance; } }
    private static GameController _instance;

    public Vector3 PlayerPosition { get { return _player.transform.position; } }

    [SerializeField] private GameObject[] _bgTiles;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _camera;
    [SerializeField] private UICanvas _uiCanvas;

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
        if (_player.transform.position.x > _camera.transform.position.x)
        {
            _camera.transform.position = new Vector3(_player.transform.position.x, 0, -20);
        }
        int playerScore = Mathf.FloorToInt(_player.transform.position.x);
        _uiCanvas.SetScore(playerScore);

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
