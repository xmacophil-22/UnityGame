using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawnables: MonoBehaviour
{
   ///////////////////////Überklasse für alle Spawnbaren Objekte
   [SerializeField] private GameObject myPrefab;
   public abstract int instantiateMe();
}
