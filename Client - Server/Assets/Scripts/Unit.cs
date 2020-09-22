using UnityEngine;
using UnityEngine.Networking;

public class Unit : NetworkBehaviour
{
    [SerializeField] protected UnitMotor _motor;
    [SerializeField] protected UnitStats _unitStats;

    protected bool _isDead;

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnAliveUpdate() { }
    protected virtual void OnDeadUpdate() { }

    protected void OnUpdate()
    {
        if(isServer)
        {
            if (!_isDead)
            {
                if(_unitStats.currHealth == 0) { Die(); }
                else { OnAliveUpdate(); }
            }
            else { OnDeadUpdate(); }
        }
    }

    [ClientCallback]
    protected virtual void Die()
    {
        _isDead = true;
        if(isServer)
        {
            _motor.MoveToPoint(transform.position);
            RpcDie();
        }
    }

    [ClientRpc]
    void RpcDie()
    {
        if (!isServer) { Die(); }
    }

    [ClientCallback]
    protected virtual void Revive()
    {
        _isDead = false;
        if(isServer)
        {
            _unitStats.SetHealthRate(1);
            RpcRevive();
        }
    }

    [ClientRpc]
    void RpcRevive()
    {
        if (!isServer) { Revive(); }
    }

}

