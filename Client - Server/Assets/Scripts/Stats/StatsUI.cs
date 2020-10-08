using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{

    #region Singleton
    public static StatsUI Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instance of StatsUI found!");
            return;
        }
        Instance = this;
    }
    #endregion

    [SerializeField] private GameObject _statsUI;
    [SerializeField] private StatItem   _damageStat;
    [SerializeField] private StatItem   _armorStat;
    [SerializeField] private StatItem   _moveSpeedStat;

    private int _curDamage;
    private int _curArmor;
    private int _curMoveSpeed;
    private StatsManager _manager;

    void Start()
    {
        _statsUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Stats"))
        {
            _statsUI.SetActive(!_statsUI.activeSelf);
        }
        if (_manager != null)
        {
            CheckManagerChanges();
        }
    }

    public void SetManager(StatsManager statsManager)
    {
        _manager = statsManager;
        CheckManagerChanges();
    }

    private void CheckManagerChanges()
    {
        if (_curDamage != _manager.Damage)
        {
            _curDamage = _manager.Damage;
            _damageStat.ChangeStat(_curDamage);
        }
        if (_curArmor != _manager.Armor)
        {
            _curArmor = _manager.Armor;
            _armorStat.ChangeStat(_curArmor);
        }
        if (_curMoveSpeed != _manager.MoveSpeed)
        {
            _curMoveSpeed = _manager.MoveSpeed;
            _moveSpeedStat.ChangeStat(_curMoveSpeed);
        }
    }
}
