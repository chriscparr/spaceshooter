using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Player.PlayerPositionChanged += OnPlayerPositionChanged;
        Player.PlayerSpawned += OnPlayerSpawned;
    }

    protected void OnPlayerSpawned(Vector3 playerPosition)
    {
        transform.position = new Vector3(playerPosition.x, 0, -20);
    }

    protected void OnPlayerPositionChanged(Vector3 playerPosition)
    {
        if (playerPosition.x > transform.position.x)
        {
            transform.position = new Vector3(playerPosition.x, 0, -20);
        }
    }
}
