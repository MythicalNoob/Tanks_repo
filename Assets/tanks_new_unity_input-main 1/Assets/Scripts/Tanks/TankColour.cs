using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankColour : MonoBehaviour
{
    public Transform meshRenderManager;

    public Material[] tankColours;

    void Start()
    {
        for(int i =0; i < meshRenderManager.childCount; i++)
        {
            var child = meshRenderManager.GetChild(i);
            MeshRenderer mesh = child.GetComponent<MeshRenderer>();
            if (mesh == null) continue;

            Material[] mats = mesh.materials;
            mats[0] = tankColours[i];
            mesh.materials = mats;
        }
    }
}
