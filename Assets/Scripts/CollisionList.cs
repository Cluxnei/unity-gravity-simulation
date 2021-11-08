using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionList : MonoBehaviour
{
    public List<GameObject> currentCollisions = new List<GameObject>();

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            return;
        }

        currentCollisions.Add(col.gameObject);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            return;
        }

        currentCollisions.Remove(col.gameObject);
    }
}
