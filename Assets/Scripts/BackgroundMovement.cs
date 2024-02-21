using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] _bgTiles;
    private List<GameObject> _bgTileList;    

    // Start is called before the first frame update
    void Start()
    {
        Player.PlayerPositionChanged += OnPlayerPositionChanged;
        _bgTileList = new List<GameObject>(_bgTiles);
        _bgTileList = _bgTileList.OrderBy(o => o.transform.position.x).ToList();
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
