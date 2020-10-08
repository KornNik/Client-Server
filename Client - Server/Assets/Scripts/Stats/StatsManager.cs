using UnityEngine;
using UnityEngine.Networking;

public class StatsManager : NetworkBehaviour
{

    [SyncVar] public int Damage, Armor, MoveSpeed;
}