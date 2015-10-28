﻿using UnityEngine;
using System.Collections;


public class CircleDraw : MonoBehaviour
{
    float theta_scale = 0.01f;        //Set lower to add more points
    int size; //Total number of points in circle
    float radius;
    
    LineRenderer lineRenderer;
    void Awake()
    {
        float sizeValue = (2.0f * Mathf.PI) / theta_scale;
        size = (int)sizeValue;
        size++;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetWidth(0.02f, 0.02f); //thickness of line
        lineRenderer.SetVertexCount(size);
        radius = SelectTest.Instance.GetComponent<Stats>().AttackRange;
    }

    void Update()
    {
        if(SelectTest.Instance.IsSelected == true)
        {
            Vector3 pos;
            float theta = 0f;
            for (int i = 0; i < size; i++)
            {
                theta += (2.0f * Mathf.PI * theta_scale);
                float x = radius * Mathf.Sin(theta);
                float z = radius * Mathf.Cos(theta);
                x += gameObject.transform.position.x;
                z += gameObject.transform.position.z;
                pos = new Vector3(x, 0, z);
                lineRenderer.SetPosition(i, pos);
            }
        }
        
    }
}
