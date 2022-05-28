using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Text _snowballCount;
    [SerializeField] Slider _guard;
    [SerializeField] Slider _health;

    Player _player;
    string _defaultText;

    void Awake()
    {

    }
    private void Start()
    {
        _defaultText = _snowballCount.text;
        _player = ObjManager.FindPlayer().GetComponent<Player>();
        _guard.maxValue = _player.Guard;
        _health.maxValue = _player.Health;
        ChangeText();
    }

    public void ChangeSliders()
    {
        _guard.value = _player.Guard;
        _health.value = _player.Health;
    }
    public void ChangeText()
    {
        _snowballCount.text = $"{_defaultText}{_player.SnowballCount}";
    }
}
