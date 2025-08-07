using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float xSpeed;
    [SerializeField] float ySpeed;
    [SerializeField] float zSpeed;

    void Update()
    {
        transform.Rotate(360 * xSpeed * Time.deltaTime, 360 * ySpeed * Time.deltaTime, 360 * zSpeed * Time.deltaTime);
    }
}
