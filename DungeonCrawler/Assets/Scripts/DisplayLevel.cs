using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayLevel : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        LevelNum.ChangeLevel(1);
        text.text = "Level " + LevelNum.level;
        Invoke(nameof(StartGame), 2f);
    }

    private void StartGame()
    {
        Destroy(gameObject);
    }
}
