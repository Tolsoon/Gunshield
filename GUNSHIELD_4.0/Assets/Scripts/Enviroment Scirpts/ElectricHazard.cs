using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricHazard : MonoBehaviour
{
    public GameObject electricHazard;
    public GameObject previewElectricHazard;

    private void Start()
    {
        StartCoroutine(Activator()); 
    }
   

    IEnumerator Activator()
    {
        electricHazard.SetActive(false);

        int rand = Random.Range(3, 8);
        

        yield return new WaitForSeconds(rand);

        previewElectricHazard.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        previewElectricHazard.SetActive(false);
        electricHazard.SetActive(true);

        yield return new WaitForSeconds(3f);
        StartCoroutine(Activator());
        electricHazard.SetActive(false);
    }
}
