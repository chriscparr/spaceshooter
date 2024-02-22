using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] _bgTiles;
    private List<GameObject> _bgTileList;

    private void Awake()
    {
        Player.PlayerPositionChanged += OnPlayerPositionChanged;
        Player.PlayerSpawned += OnPlayerSpawned;
        _bgTileList = new List<GameObject>(_bgTiles);
    }

    protected void OnPlayerSpawned(Vector3 playerPosition)
    {
        _bgTileList[0].transform.position = new Vector3(-50f, 0, 50);
        _bgTileList[1].transform.position = new Vector3(50f, 0, 50);
        _bgTileList[2].transform.position = new Vector3(150f, 0, 50);
    }

    protected void OnPlayerPositionChanged(Vector3 playerPosition)
    {
        if (playerPosition.x - _bgTileList[0].transform.position.x >= 150f)
        {
            _bgTileList[0].transform.position = _bgTileList[_bgTileList.Count - 1].transform.position + new Vector3(100f, 0f, 0f);
            _bgTileList = _bgTileList.OrderBy(o => o.transform.position.x).ToList();
        }
    }
}
