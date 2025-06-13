using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    public GameObject cubo;
    public Transform offset;

    private bool activo;


    void Update()
    {
        if (activo == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                cubo.transform.SetParent(offset);
                cubo.transform.position = offset.position;
                cubo.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            cubo.transform.SetParent(null);
            cubo.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            activo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activo = false;
        }
    }
}
