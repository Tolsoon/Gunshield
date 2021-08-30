using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunStatsShow : MonoBehaviour
{
    public Slider damage;
    public Slider fireRate;
    public Slider energyCost;
    public Slider bulletSpeed;
    public Slider bounce;
    

    public GameObject gun;

    public float damageStat;
    public float fireRateStat;
    public float energyCostStat;
    public float bulletSpeedStat;
    public float bounceStat;




    public void Update()
    {




        damage.value = damageStat;
        fireRate.value = fireRateStat;
        energyCost.value = energyCostStat;
        bulletSpeed.value = bulletSpeedStat;
        bounce.value = bounceStat;
    }
}
