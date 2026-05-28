using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour, IInvisibleForBuff
{
    public ChunkSpeed ChunkSpeed;
    public Car Car;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            Car.Crush();
            other.GetComponent<CarHitAnimation>().PlayHitAnimation(transform);
            ChunkSpeed.ReduceSpeedAfterCrush();
        }
    }

    public void SetInvisible(bool invisible)
    {
        GetComponent<Collider>().enabled = !invisible;
    }
}
