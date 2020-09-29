using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHider : MonoBehaviour
{
    public MeshRenderer[] meshes;

    //Add all the meshes in the component
    private void Awake()
    {
        meshes = GetComponentsInChildren<MeshRenderer>();
    }

    //Active all the meshes
    public void ShowMeshes()
    {
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.enabled = true;
        }
    }

    //Deactive All the meshes
    public void HideMeshes()
    {
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }
    }

}
