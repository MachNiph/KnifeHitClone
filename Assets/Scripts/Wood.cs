using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wood : MonoBehaviour
{

    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private WoodData[] woodData;

    [SerializeField]
    private float currentAngle;

    [SerializeField]
    private int index = 0;

    private GameObject wood;

    private void Start()
    {
       
        if (woodData.Length > 0)
        {
            currentAngle = woodData[0].rotationAngle;
        }
    }

    private void Update()
    {

       
        if (woodData.Length > 0 )
        {
            currentAngle = Mathf.MoveTowards(currentAngle, woodData[index].rotationAngle, rotationSpeed * Time.deltaTime * woodData[index].speedMultiplier);
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);
            if (currentAngle == woodData[index].rotationAngle)
            {
                index = (index + 1) % woodData.Length;
            }
        }
        else
        {
            transform.Rotate(new Vector3(0,0, rotationSpeed * Time.deltaTime));
        }
    }
}