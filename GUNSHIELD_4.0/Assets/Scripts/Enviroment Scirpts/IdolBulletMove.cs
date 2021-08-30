using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolBulletMove : MonoBehaviour
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

            if (hit.transform.gameObject.CompareTag("robot"))
            {                
                hit.transform.gameObject.GetComponent<MoveTest3>().audioSource.clip = hit.transform.gameObject.GetComponent<MoveTest3>().sfx[2];
                hit.transform.gameObject.GetComponent<MoveTest3>().audioSource.Play();

                Destroy(gameObject);
            }


        }

        if (life < 0)
        {
            Destroy(gameObject);
        }



    }
}
