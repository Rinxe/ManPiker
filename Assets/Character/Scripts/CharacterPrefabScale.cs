using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterPrefabScale : MonoBehaviour
{
    // không thể kế thừa được kiểu float nên giá trị này sẽ gấp 10 lần scale, khi được call thì nó phải được nhân với 0.1f
    // đây là giá trị mà prefab được scale lên khi được spawn
    public enum ScaleOfPrefabs
    {
        ALPHABET = 20,
        HUMANOID = 120,
        EXTREME = 100,
        LONGARM = 10,
        MONSTER_6 = 6,
    }

    [SerializeField] private ScaleOfPrefabs scaleOfPrefabs;

    [FormerlySerializedAs("skin")] [SerializeField] private SkinnedMeshRenderer[] skins;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private Material outLightMaterial;

    public void TurnOnNormalMaterial()
    {
        if (!normalMaterial) return;
        foreach (var skin in skins)
        {
            skin.sharedMaterial = normalMaterial;
        }
    }
    
    public void TurnOnOutlineMaterial()
    {
        if (!outlineMaterial) return;
        foreach (var skin in skins)
        {
            skin.sharedMaterial = outlineMaterial;
        }
    }

    public void TurnOnOutLightMaterial()
    {
        if (!outLightMaterial) return;
        foreach (var skin in skins)
        {
            skin.sharedMaterial = outLightMaterial;
        }
    }
    
    public float ValueOfScale => (int)scaleOfPrefabs;
    public ScaleOfPrefabs EnumOfPrefabs => scaleOfPrefabs;
}
