using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreboardItem : MonoBehaviour{
    [SerializeField] private Text usernameText;
    [SerializeField] private Text deathText;

    public void Setup(string username, int deaths){
        usernameText.text = username;
        deathText.text = "Deaths: " + deaths;
    }
}