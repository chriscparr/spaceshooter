using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected Bullet bulletPrefab;
    protected float _moveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //Get the value of the Horizontal input axis.

        float verticalInput = Input.GetAxis("Vertical");
        //Get the value of the Vertical input axis.

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _moveSpeed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Bullet bull = Instantiate<Bullet>(bulletPrefab);
            bull.transform.position = transform.position + Vector3.right;
            bull.Shoot(Vector3.right, 20f);
        }

    }
}
