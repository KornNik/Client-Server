using UnityEngine;
using UnityEngine.Networking;

public class UnitStats : NetworkBehaviour
{
    [SerializeField] private int _maxHealth;
    [SyncVar] private int _currHealth;

    public Stat Damage;

    public int currHealth { get { return _currHealth; } }

    public override void OnStartServer()
    {
        _currHealth = _maxHealth;
    }

    public void SetHealthRate(float rate)
    {
        _currHealth = rate == 0 ? 0 : (int)(_maxHealth / rate);
    }
}

