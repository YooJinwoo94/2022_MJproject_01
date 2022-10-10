using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSceneBGScrollManager : MonoBehaviour
{
    [SerializeField]
    MeshRenderer []render;

    [SerializeField]
    float speed;
    [HideInInspector]
    public float offSet;


    public bool moveStart = false;

    private void Update()
    {
        if (moveStart == false) return;


        offSet += Time.deltaTime * speed;

        for(int i = 0; i<render.Length;i++)
        {
            render[i].material.mainTextureOffset = new Vector2(offSet, 0);
        }
     
    }
}
