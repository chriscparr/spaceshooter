using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Vector3 newPosition = new Vector3(transform.position.x + 20f, Random.Range(Constants.MinY, Constants.MaxY), 0f);
        transform.position = newPosition;
    }
}
