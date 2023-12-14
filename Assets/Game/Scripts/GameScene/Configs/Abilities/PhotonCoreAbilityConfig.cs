using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(
    fileName = "PhotonCoreAbilityConfig",
    menuName = "Configs/Ability/PhotonCoreAbilityConfig",
    order = 0)]
public class PhotonCoreAbilityConfig : ScriptableObject
{
    [Min(1)]
    public int MaxLevel;
    public float SerchRadius;
    public float EndReloadTime;
    public float ReloadTimeMultiplier;
    public LayerMask TargetLayerMask;

    [Space]
    public LevelData[] LevelsData;


    public LevelData GetLevelData(int level)
    {
        foreach (var levelData in LevelsData)
        {
            if (levelData.Level == level) return levelData;
        }

        throw new ArgumentException($"Wrong level({level})");
    }

    private void OnValidate()
    {
        var oldData = LevelsData;
        LevelsData = new LevelData[MaxLevel];

        for (int i = 0; i < LevelsData.Length; i++)
        {
            if (i < oldData.Length)
            {
                LevelsData[i] = oldData[i];
            }

            LevelsData[i].Level = i + 1;
        }

        for (int i = LevelsData.Length - 1; i >= 0; i--)
        {
            if (i == LevelsData.Length - 1)
            {
                LevelsData[i].ReloadTime = EndReloadTime;
            }
            else
            {
                LevelsData[i].ReloadTime = LevelsData[i + 1].ReloadTime * ReloadTimeMultiplier;
            }
        }
    }


    [Serializable]
    public struct LevelData
    {
        [Space]
        [ReadOnly]
        public int Level;

        [ReadOnly]
        public float ReloadTime;
        public int Damage;
    }
}