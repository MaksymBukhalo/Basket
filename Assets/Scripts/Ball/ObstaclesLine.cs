using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesLine : MonoBehaviour
{
    public Animation startLineObstale;

    private void Start()
    {
        startLineObstale.Play("ObstacleLineAnimation");
    }
}
