using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSceneMain : MonoBehaviour
{
    [SerializeField] FightAssetContext FightAssetContext;
    [SerializeField] FightSceneContext FightSceneContext;
    [SerializeField] FightPhaseRunner FightPhaseRunner;

    void Start()
    {
        FightAssetContext.Init();
        FightSceneContext.Init(FightAssetContext);
        FightPhaseRunner.Init(FightAssetContext.GetFightState());
        FightPhaseRunner.Run();
    }

}
