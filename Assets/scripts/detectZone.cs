using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class detectZone : MonoBehaviour
{
    
    Collider2D col;
    public List<Collider2D> targetList = new List<Collider2D>();

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetList.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        targetList.Remove(collision);
    }

}
