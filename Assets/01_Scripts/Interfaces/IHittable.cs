using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public int Health { get; set; }

    public void GetHit(GameObject dealer, int damage,int neutralTime); 
}
