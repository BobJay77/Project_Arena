using UnityEngine;
using System.Linq;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;

public class LeaderBoard : MonoBehaviour
{
    private static LeaderBoard _instance;

    public GameObject playersHolder_;

    [Header("Options")]
    public float refreshRate_ = 1f;

    [Header("UI")]
    public GameObject[] slots_;
    public TextMeshProUGUI[] scoreTexts_;
    public TextMeshProUGUI[] nameText_;

    #region Properties

    public static LeaderBoard _Instance
    {
        get { return _instance; }
    }
    #endregion

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(_instance);

        InitialteLeaderBoard();
    }

    public void InitialteLeaderBoard()
    {
        InvokeRepeating(nameof(Refresh), 1f, refreshRate_);
    }

    public void Refresh()
    {

        foreach(var slot in slots_)
        {
            slot.SetActive(false);
        }

        var sortedPlayerList = (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();

        int i = 0;

        foreach(var player in sortedPlayerList) 
        {
            slots_[i].SetActive(true);
            
            if(player.NickName == "")
            {
                player.NickName = "unnamed";
            }

            nameText_[i].text = player.NickName;
            scoreTexts_[i].text = player.GetScore().ToString();

            i++;
        }
    }

    public void ShowLeaderBoard(bool leaderBoardInput)
    {
            playersHolder_.SetActive(leaderBoardInput);   
    }
}
