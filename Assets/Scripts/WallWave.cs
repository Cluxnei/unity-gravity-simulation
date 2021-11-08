using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWave : MonoBehaviour
{
    private float wallWaveInterval = 30f;
    public float pushForce = 90f;


    void Start()
    {
        StartCoroutine(this.wallWaves());
    }

    IEnumerator wallWaves()
    {
        while(true)
        {
            yield return new WaitForSeconds(this.wallWaveInterval);
            this.wallWave();
        }
    }
    
    void wallWave()
    {
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            CollisionList cl = wall.GetComponent<CollisionList>();
            Vector2 force = Vector2.up;
            if (wall.name == "Top")
            {
                force = Vector2.down;
            }
            if (wall.name == "Left")
            {
                force = Vector2.right;
            }
            if (wall.name == "Right")
            {
                force = Vector2.left;
            }
            force *= this.pushForce;

            foreach (GameObject o in cl.currentCollisions)
            {
                o.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            }
        }
    }

}
