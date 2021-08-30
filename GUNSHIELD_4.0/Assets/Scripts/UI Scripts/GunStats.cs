using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunStats : MonoBehaviour
{
    public Slider damage;
    public Slider fireRate;
    public Slider energyCost;
    public Slider bulletSpeed;
    public Slider bounce;
    public Text gunName;

    public GameObject gun;
    public GameObject[] guns;

    public loadoutSelection loadout;

    public void Update()
    {
        gun = loadout.guns[loadout.selectedGun];

        gunName.text = gun.GetComponent<gun>().gunName;

        damage.value = gun.GetComponent<gun>().damage;
        fireRate.value = gun.GetComponent<gun>().fireRate;
        energyCost.value = gun.GetComponent<gun>().energyCost;
        bulletSpeed.value = gun.GetComponent<gun>().bulletSpeed;
        bounce.value = gun.GetComponent<gun>().bounce;
    }

}
