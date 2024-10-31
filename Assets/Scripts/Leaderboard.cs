using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]private List<TextMeshProUGUI> names = new List<TextMeshProUGUI>();  
    [SerializeField]private List<TextMeshProUGUI> scores = new List<TextMeshProUGUI>();
    [SerializeField]private string[] badWords; 
    [SerializeField]private TextAsset badWordsFile;

    [SerializeField]private TMP_InputField nameInput;

    private string publicLeaderboardKey = "1dda0fb7ef2cfba2e4f2ad05e0f297f487a133261d471150d52e9ed45606c96f";

    private void Start() {
        GetLeaderboard();

        badWords = badWordsFile.text.Split();

        List<int> badWordsIndex = new List<int>();

        var filtered = badWords.Where(s=> !string.IsNullOrWhiteSpace(s)).ToArray();
        badWords = filtered;
    }

    public void GetLeaderboard(){
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((value) => {
            int loopLength = (value.Length < name.Count()) ? value.Length : names.Count;
            for (int i = 0; i < loopLength; i++) {
                names[i].text = value[i].Username;
                scores[i].text = value[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, float score) {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, (int)score, ((value) => {
            if (System.Array.IndexOf(badWords, name) != -1) return;
            GetLeaderboard();
        }));
    }

    public void SubmitScore(){
        SetLeaderboardEntry(nameInput.text, GameObject.Find("DontDestroyOnLoad").GetComponent<DontDestroyOnLoadScript>().score);
    } 
}
