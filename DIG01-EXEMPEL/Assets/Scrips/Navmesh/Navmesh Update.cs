using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavmeshUpdate : MonoBehaviour
{
    static void UpdateNavMesh(NavMeshSurface surface2D)
    {
         surface2D.UpdateNavMesh(surface2D.navMeshData);
    }
}
