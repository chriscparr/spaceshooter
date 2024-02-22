using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 initialCameraPosition = new Vector3(0f, 0f, -20f);
    void Start()
    {
        Player.PlayerPositionChanged += OnPlayerPositionChanged;
        Player.PlayerSpawned += OnPlayerSpawned;
    }

    protected void OnPlayerSpawned(Vector3 playerPosition)
    {
        transform.position = initialCameraPosition + new Vector3(playerPosition.x, 0f, 0f);
    }

    protected void OnPlayerPositionChanged(Vector3 playerPosition)
    {
        if (playerPosition.x > transform.position.x)
        {
            transform.position = initialCameraPosition + new Vector3(playerPosition.x, 0f, 0f);
        }
    }
}
