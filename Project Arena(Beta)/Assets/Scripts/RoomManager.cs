using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    //Change these to not be public and add properties for proper set up
    private static RoomManager _instance;

    public GameObject player;

    public Transform[] spawnPoints;

    public GameObject roomCam;

    public GameObject nameUI;

    public GameObject connectingUI;

    private string _nickName = "unnamed";

    public static RoomManager _Instance
    {
        get { return _instance; }
    }

    private void Awake()
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
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("Test", null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("We are connected and in a room!");

        roomCam.SetActive(false);

       SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];

        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<FPSPlayerStats>().IsLocalPlayer();

        _player.GetComponent<PhotonView>().RPC("SetPlayerNickName", RpcTarget.AllBuffered, _nickName);
        PhotonNetwork.LocalPlayer.NickName = _nickName;
    }

    public void ChangeNickName(string name)
    {
        _nickName = name;
    }
    public void JoinButtonPressed()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.ConnectUsingSettings();

        nameUI.SetActive(false);
        connectingUI.SetActive(true);
    }
}
