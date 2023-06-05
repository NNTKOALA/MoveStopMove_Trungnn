using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    private float yPos;
    private Vector3 startPos;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public void SetYPosition(float yPosition)
    {
        yPos = yPosition;
    }
}
