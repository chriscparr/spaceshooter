using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public delegate void PlayerPositionStatus(Vector3 position);
    public event PlayerPositionStatus PlayerPositionChanged;
    public static GameController Instance { get { return _instance; } }
    private static GameController _instance;

    [SerializeField] private Player _player;

    private void Awake()
    {
        _instance = this;
    }


    private void Update()
    {
        PlayerPositionChanged?.Invoke(_player.transform.position);
    }

}
