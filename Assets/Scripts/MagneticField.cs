using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
    [SerializeField] float magneticForce = default;
    [SerializeField] Color goldColor = default;
    public static event Action CubeEntered;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            other.gameObject.layer = 9;
            other.attachedRigidbody.velocity = (transform.position - other.transform.position) * magneticForce;
            other.gameObject.LeanColor(goldColor, 0.1f);
            CubeEntered?.Invoke();
        }
    }
}
