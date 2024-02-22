using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractGameEntity
{
    public delegate void PlayerPositionStatus(Vector3 position);
    public static event PlayerPositionStatus PlayerPositionChanged;
    public static event PlayerPositionStatus PlayerKilled;
    public static event PlayerPositionStatus PlayerSpawned;

    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Renderer renderer;
    [SerializeField] protected Collider collider;

    protected override int Health { get => _health; set => _health = value; }
    protected override int TotalHealth => 10;
    protected override int bulletInterval => 250;
    protected int _health;
    protected float _moveSpeed = 1;

    protected Vector3 shootTarget;

    protected void Awake()
    {
        EnemySpawner.SpawnPositionsChanged += OnSpawnPositionsChanged;
    }

    protected override void Start()
    {
        base.Start();
        PlayerSpawned?.Invoke(transform.position);
    }

    private void OnSpawnPositionsChanged(List<Vector3> spawnPositions)
    {
        spawnPositions.Sort(delegate (Vector3 a, Vector3 b)
        {
            return Vector3.Distance(transform.position, a).CompareTo(Vector3.Distance(transform.position, b));
        });
        shootTarget = spawnPositions[0];
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(new Vector3(horizontalInput, verticalInput, 0) * _moveSpeed, ForceMode.Force);
        PlayerPositionChanged?.Invoke(transform.position);
    }

    protected override void Shoot()
    {
        if (bulletCountdown == 0)
        {
            bulletCountdown = bulletInterval;
            Bullet bullet = PoolingManager.Instance.GetObjectFromPool("Bullet", true, 0).GetComponent<Bullet>();
            bullet.transform.position = transform.position + Vector3.right;
            Vector3 shootDirection = (shootTarget - transform.position).normalized;
            bullet.FireBullet(shootDirection, 20f);
        }
        if (bulletCountdown > 0)
        {
            bulletCountdown--;
        }
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            PlayerKilled?.Invoke(transform.position);
            _rigidbody.velocity = Vector3.zero;
            renderer.enabled = false;
            collider.enabled = false;
            _ = StartCoroutine(SpawnWait());
        }
    }

    private void SpawnPlayer()
    {
        renderer.enabled = true;
        Health = TotalHealth;
        transform.position = Vector3.zero;
        collider.enabled = true;
        PlayerSpawned?.Invoke(transform.position);
    }

    private IEnumerator SpawnWait()
    {
        yield return new WaitForSeconds(2f);
        SpawnPlayer();
        yield return null;
    }
}
