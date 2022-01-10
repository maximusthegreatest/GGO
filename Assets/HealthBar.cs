using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player player;

    private float _maxHealth;
    private float _currentHealth;
    private Image _healthBar;


    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = player.health;
        _healthBar = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        _currentHealth = player.health;
        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }
}
