using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveTurret : MonoBehaviour
{
    public float speed;
    public float life;
    public float damage;
    public float damageMultiplier = 1.2f;
    public float energyCost;

    public List<AudioClip> sounds;

    public GameObject whoShot;

    public gun Gun;

    public loadoutSelection loadout;

    AudioSource audioSource;

    
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    void FixedUpdate()
    {


        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Time.deltaTime * speed))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rat = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rat, 0);


            if (hit.transform.gameObject.CompareTag("wall"))
            {
                audioSource.clip = sounds[0];
                audioSource.Play();
                life -= 1;
                damage *= damageMultiplier;
            }

            if (hit.transform.gameObject.CompareTag("shield"))
            {
                audioSource.clip = sounds[1];
                audioSource.Play();
                life -= 1;
                damage *= damageMultiplier;

                float energyRestore = energyCost / 2f;
                float difference = hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().maxEnergyCapacity - hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().energyCapacity;

                if (difference >= energyRestore)
                {
                    hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().energyCapacity += energyRestore;
                }
                else if (difference < energyRestore)
                {

                    float leftover = energyRestore - difference;

                    hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().energyCapacity += difference;
                    hit.transform.gameObject.GetComponent<shield>().headPlate.GetComponentInChildren<Robot>().overchargeCapacity += leftover;
                }


            }

            if (hit.transform.gameObject.CompareTag("robot"))
            {
                if (hit.transform.gameObject.GetComponent<MoveTest3>().invincible == false)
                {
                    hit.transform.gameObject.GetComponentInChildren<Robot>().health -= damage;
                    hit.transform.gameObject.GetComponent<MoveTest3>().audioSource.clip = hit.transform.gameObject.GetComponent<MoveTest3>().sfx[2];
                    hit.transform.gameObject.GetComponent<MoveTest3>().audioSource.Play();
                }
                


                Destroy(gameObject);
            }


        }

        if (life < 0)
        {
            Destroy(gameObject);
        }



    }

}
