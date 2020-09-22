using UnityEngine;

[RequireComponent(typeof(UnitMotor), typeof(PlayerStats))]
public class Character : Unit
{
    [SerializeField] private float _reviveDelay = 5f;
    [SerializeField] private GameObject _gfx;

    private Vector3 _startPosition;
    private float _reviveTime;

    private void Start()
    {
        _startPosition = transform.position;
        _reviveTime = _reviveDelay;
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

    protected override void Die()
    {
        base.Die();
        _gfx.SetActive(false);
    }

    protected override void Revive()
    {
        base.Revive();
        transform.position = _startPosition;
        _gfx.SetActive(true);
        if (isServer) { _motor.MoveToPoint(_startPosition); }
    }

    public void SetMovePoint(Vector3 point)
    {
        if (!_isDead) { _motor.MoveToPoint(point); }
    }

}
