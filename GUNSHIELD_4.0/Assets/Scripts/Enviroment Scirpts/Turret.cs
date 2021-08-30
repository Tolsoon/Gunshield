
using System.Collections;

using UnityEngine;

public class Turret : MonoBehaviour
{
    public float speed;

    public bool calculating = false;

    public GameObject bullet;
    public Transform firespot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);

        if (calculating == false)
        {
            StartCoroutine(ShouldShoot());
        }

    }

    IEnumerator ShouldShoot()
    {
        calculating = true;
        int num = Random.Range(0, 2);
        Debug.Log(num);
        if(num == 0)
        {
            //shoot
            GameObject bulletClone = Instantiate(bullet, firespot.position, firespot.rotation);
        }
        else
        {
            GameObject bulletClone = Instantiate(bullet, firespot.position, firespot.rotation);
        }

        
        yield return new WaitForSeconds(7f);
        calculating = false;
    }
}
