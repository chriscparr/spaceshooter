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
    [SerializeField] protected Renderer _renderer;
    [SerializeField] protected Collider _collider;

    protected override int Health { get => _health; set => _health = value; }
    protected override int TotalHealth => 10;
    protected override float bulletInterval => 1f;

    protected override float bulletSpeed => 20f;

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
        SpawnPlayer();
    }

    private void OnSpawnPositionsChanged(List<Vector3> spawnPositions)
    {
        spawnPositions.Sort(delegate (Vector3 a, Vector3 b)
        {
            return Vector3.Distance(transform.position, a).CompareTo(Vector3.Distance(transform.position, b));
        });
        shootTarget = spawnPositions[0];
    }

    protected void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(new Vector3(horizontalInput, verticalInput, 0) * _moveSpeed, ForceMode.Force);
        PlayerPositionChanged?.Invoke(transform.position);
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {            
            StopCoroutine(ShootInterval());
            PlayerKilled?.Invoke(transform.position);
            _rigidbody.velocity = Vector3.zero;
            _renderer.enabled = false;
            _collider.enabled = false;
            _ = StartCoroutine(SpawnWait());
        }
    }

    private void SpawnPlayer()
    {
        _renderer.enabled = true;
        Health = TotalHealth;
        transform.position = Vector3.zero;
        _collider.enabled = true;
        PlayerSpawned?.Invoke(transform.position);
        StartCoroutine(ShootInterval());
    }

    private IEnumerator SpawnWait()
    {
        yield return new WaitForSeconds(2f);
        SpawnPlayer();
        yield return null;
    }

    protected override void Shoot()
    {
        Bullet bullet = PoolingManager.Instance.GetObjectFromPool(Constants.BULLET_PREFAB_NAME, true, 0).GetComponent<Bullet>();
        Vector3 bulletStartPosition = transform.position + new Vector3(1.3f, 0f, 0f);
        Vector3 shootDirection = (shootTarget - bulletStartPosition).normalized;
        bullet.transform.position = bulletStartPosition;      
        bullet.FireBullet(shootDirection, bulletSpeed);
    }
}
