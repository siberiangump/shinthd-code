using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour, IStateView
{
    [SerializeField] Renderer Renderer;
    [SerializeField] MaterialPresetData MaterialPresetData;

    public void SetState(ViewStateEnum state)
    {
        Renderer.material = MaterialPresetData.GetMaterial(state);
    }
}
