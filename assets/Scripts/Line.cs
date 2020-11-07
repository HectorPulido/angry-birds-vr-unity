using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private Vector3[] cachePosition;
    private LineRenderer line;

    public Transform leftPivot;
    public Transform rightPivot;
    public Transform bird;

    public bool birdInLine;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();

        cachePosition = new Vector3[]
        {
            leftPivot.position,
            rightPivot.position
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (birdInLine)
        {
            var linePositions = new Vector3[]
            {
                leftPivot.position,
                bird.position,
                rightPivot.position
            };
            line.SetPositions(linePositions);
        }
        else
        {
            line.SetPositions(cachePosition);
        }
    }
}
