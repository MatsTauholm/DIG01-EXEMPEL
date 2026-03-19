using UnityEngine;
using Unity.Cinemachine;

public class SplitScreen : MonoBehaviour
{
    public CinemachineBrain brain;
    public ICinemachineCamera cam1;
    public ICinemachineCamera cam2;

    private void Start()
    {
        cam1 = GetComponent<CinemachineCamera>();
        cam2 = GetComponent<CinemachineCamera>();

        int layer = 1;
        int priority = 1;
        float weight = 1f;
        float blendTime = 0f;
        brain.SetCameraOverride(layer, priority, cam1, cam2, weight, blendTime);
    }
}
