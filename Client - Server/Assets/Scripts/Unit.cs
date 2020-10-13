﻿using UnityEngine;
using UnityEngine.Networking;

public class Unit : Interactable
{
    [SerializeField] protected UnitMotor _motor;
    [SerializeField] protected UnitStats _unitStats;

    protected Interactable _focus;
    protected bool _isDead;

    public delegate void UnitDelegate();
    [SyncEvent] public event UnitDelegate EventOnDamage;
    [SyncEvent] public event UnitDelegate EventOnDie;
    [SyncEvent] public event UnitDelegate EventOnRevive;

    public UnitStats UnitStats { get { return _unitStats; } }

    public override void OnStartServer()
    {
        _motor.SetMoveSpeed(_unitStats.MoveSpeed.GetValue());
        _unitStats.MoveSpeed.OnStatChanged += _motor.SetMoveSpeed;
    }

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

    protected virtual void SetFocus(Interactable newFocus)
    {
        if(newFocus!=_focus)
        {
            _focus = newFocus;
            _motor.FollowTarget(newFocus);
        }
    }

    protected virtual void RemoveFocus()
    {
        _focus = null;
        _motor.StopFollowingTarget();
    }
    protected virtual void DamageWithCombat(GameObject user)
    {
        EventOnDamage();
    }

    public override bool Interact(GameObject user)
    {
        Combat combat = user.GetComponent<Combat>();
        if (combat != null)
        {
            if (combat.Attack(_unitStats)) { DamageWithCombat(user); }
            return true;
        }
        return base.Interact(user);
    }

    [ClientCallback]
    protected virtual void Die()
    {
        _isDead = true;
        GetComponent<Collider>().enabled = false;
        if (isServer)
        {
            HasInteracte = false;
            RemoveFocus();
            _motor.MoveToPoint(transform.position);
            EventOnDie();
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
        GetComponent<Collider>().enabled = true;
        if (isServer)
        {
            HasInteracte = true;
            _unitStats.SetHealthRate(1);
            EventOnRevive();
            RpcRevive();
        }
    }

    [ClientRpc]
    void RpcRevive()
    {
        if (!isServer) { Revive(); }
    }

}

