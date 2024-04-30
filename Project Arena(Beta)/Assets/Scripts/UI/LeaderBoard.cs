using UnityEngine;
using System.Linq;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;

public class LeaderBoard : MonoBehaviour
{
    public GameObject playersHolder_;

    [Header("Options")]
    public float refreshRate_ = 1f;

    [Header("UI")]
    public GameObject[] slots_;
    public TextMeshProUGUI[] scoreTexts_;
    public TextMeshProUGUI[] nameText_;

    private void Start()
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

    private void Update()
    {
        //Change this to new input system
            playersHolder_.SetActive(Input.GetKey(KeyCode.Tab));
        
    }
}
