using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    
    public int PointValue;

    void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        switch (PointValue)
        {
            case 1 :
                //block.SetColor("_BaseColor", Color.green);
                block.SetColor("_BaseColor", PersistentData.Instance.setOfColors[0]);
                break;
            case 2:
                //block.SetColor("_BaseColor", Color.yellow);
                block.SetColor("_BaseColor", PersistentData.Instance.setOfColors[1]);
                break;
            case 5:
                //block.SetColor("_BaseColor", Color.blue);
                block.SetColor("_BaseColor", PersistentData.Instance.setOfColors[2]);
                break;
            default:
                //block.SetColor("_BaseColor", Color.red);
                block.SetColor("_BaseColor", PersistentData.Instance.setOfColors[3]);
                break;
        }
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        onDestroyed.Invoke(PointValue);
        
        //slight delay to be sure the ball has time to bounce
        Destroy(gameObject, 0.2f);
    }
}
