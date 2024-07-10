using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileColor", menuName = "ScriptableObjects/TileColor", order = 1)]
public class TileColor_SO : ScriptableObject
{
    public SerializableDictionary<long, Color32> TileColorDict = new SerializableDictionary<long, Color32>
    {
        { 2, new Color32(255, 255, 255, 255) },
        { 4, new Color32(238, 228, 218, 255) },
        { 8, new Color32(242, 177, 121, 255) },
        { 16, new Color32(245, 149, 99, 255) },
        { 32, new Color32(246, 124, 95, 255) },
        { 64, new Color32(246, 94, 59, 255) },
        { 128, new Color32(237, 207, 114, 255) },
        { 256, new Color32(237, 204, 97, 255) },
        { 512, new Color32(237, 200, 80, 255) },
        { 1024, new Color32(237, 197, 63, 255) },
        { 2048, new Color32(237, 194, 46, 255) },
        { 4096, new Color32(206, 193, 241, 255) },
        { 8192, new Color32(178, 158, 218, 255) },
        { 16384, new Color32(158, 129, 203, 255) },
        { 32768, new Color32(174, 224, 224, 255) },
        { 65536, new Color32(135, 206, 206, 255) },
        { 131072, new Color32(95, 188, 188, 255) },
        { 262144, new Color32(210, 231, 255, 255) },
        { 524288, new Color32(174, 214, 241, 255) },
        { 1048576, new Color32(133, 193, 233, 255) },
        { 2097152, new Color32(162, 217, 206, 255) },
        { 4194304, new Color32(77, 182, 172, 255) },
        { 8388608, new Color32(38, 166, 154, 255) },
        { 16777216, new Color32(244, 143, 177, 255) },
        { 33554432, new Color32(236, 64, 122, 255) },
        { 67108864, new Color32(216, 27, 96, 255) },
        { 134217728, new Color32(215, 204, 200, 255) },
        { 268435456, new Color32(188, 170, 164, 255) },
    };
}