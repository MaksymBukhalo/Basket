using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    private float width;
    private float height;
    private RectTransform canvasSize;

    [SerializeField] private GameObject wallTop;
    [SerializeField] private GameObject wallLeft;
    [SerializeField] private GameObject wallRight;
    [SerializeField] private GameObject wallBottom;

    void Start()
    {
        string nameFind = "Canvas";
        canvasSize = GameObject.Find(nameFind).GetComponent<RectTransform>();
        width = canvasSize.sizeDelta.x;
        height = canvasSize.sizeDelta.y;
        SetPpointsEdgeCollider2DTopOrBottom(wallBottom);
        SetPpointsEdgeCollider2DLeftOrRight(wallRight);
        SetPpointsEdgeCollider2DLeftOrRight(wallLeft);
    }

    private void SetPpointsEdgeCollider2DTopOrBottom(GameObject TopOrBottom)
    {
        EdgeCollider2D edgeCollider2 = TopOrBottom.GetComponent<EdgeCollider2D>();
        Vector2[] vectors = edgeCollider2.points;
        vectors[0].x = width/2 * -1;
        vectors[1].x = width/2;
        edgeCollider2.points = vectors;
    }

    private void SetPpointsEdgeCollider2DLeftOrRight(GameObject LeftOrRight)
    {
        EdgeCollider2D edgeCollider2 = LeftOrRight.GetComponent<EdgeCollider2D>();
        Vector2[] vectors = edgeCollider2.points;
        vectors[0].y = height/2 * -1;
        vectors[1].y = height/2;
        edgeCollider2.points = vectors;
    }
}
