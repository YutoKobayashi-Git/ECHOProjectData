using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Display : MonoBehaviour
{

    TextMeshProUGUI gameUiTextTime;
    TextMeshProUGUI gameUiTextScore;

    // Start is called before the first frame update
    void Start()
    {

        gameUiTextTime = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        gameUiTextScore = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();

        gameUiTextTime.text = "Time : 1200"; 
        gameUiTextScore.text = "Score : 5000";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
