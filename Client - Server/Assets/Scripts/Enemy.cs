using UnityEngine;

[RequireComponent(typeof(UnitMotor), typeof(EnemyStats))]
public class Enemy : Unit
{
    [Header("Movement")]
    [SerializeField] private float _moveRadius = 10f;
    [SerializeField] private float _minMoveDelay = 4f;
    [SerializeField] private float _maxMoveDelay = 12f;
    private Vector3 _startPosition;
    private Vector3 _currentDistanation;
    private float _changePositionTime;

    [Header("Behavioure")]
    [SerializeField] private bool _isAggressive;
    [SerializeField] private float _viewDistance;
    [SerializeField] private float _reviveDelay;
    private float _reviveTime;

    private void Start()
    {
        _startPosition = transform.position;
        _reviveTime = _reviveDelay;
        _changePositionTime = Random.Range(_minMoveDelay, _maxMoveDelay);
    }

    private void Update()
    {
        OnUpdate();
    }

    protected override void OnDeadUpdate()
    {
        base.OnDeadUpdate();
        if (_reviveTime > 0) { _reviveTime -= Time.deltaTime; }
        else { _reviveTime = _reviveDelay; Revive(); }
    }

    protected override void OnAliveUpdate()
    {
        base.OnAliveUpdate();
        Wandering(Time.deltaTime);
    }

    protected override void Revive()
    {
        base.Revive();
        transform.position = _startPosition;
        if (isServer) { _motor.MoveToPoint(_startPosition); }
    }

    private void Wandering(float deltaTime)
    {
        _changePositionTime -= deltaTime;
        if (_changePositionTime <= 0)
        {
            RandomMove();
            _changePositionTime = Random.Range(_minMoveDelay, _maxMoveDelay);
        }
    }
    
    private void RandomMove()
    {
        _currentDistanation = Quaternion.AngleAxis(Random.Range(0f, 360f), 
            Vector3.up) * new Vector3(_moveRadius, 0, 0) + _startPosition;
        _motor.MoveToPoint(_currentDistanation);
    }

}