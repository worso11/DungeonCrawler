using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNum : MonoBehaviour
{
    public static int level;

    public static void ChangeLevel(int i)
    {
        level += i;
    }
}
