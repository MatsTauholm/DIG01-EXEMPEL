using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavmeshUpdate : MonoBehaviour
{
    public NavMeshSurface surface2D;

    void Update()
    {
        surface2D.UpdateNavMesh(surface2D.navMeshData);
    }
}
