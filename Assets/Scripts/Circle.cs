using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public Vector3 movDir;
    public int score = 0;
    int r = 28;
    
    void Update()
    {
        transform.position += movDir / movDir.magnitude * (300 + score * 25) * Time.deltaTime;
        if (transform.position.x > 720 | transform.position.x < 0 |
            transform.position.y < 0 | transform.position.y > 1280)
            movDir *= -1;
    }
    public bool onPoint(Vector2 point)
    {
        if(Mathf.Pow(point.x - transform.position.x, 2) + Mathf.Pow(point.y - transform.position.y, 2) <= Mathf.Pow(r,2))
            return true;
        return false;
    }
}
