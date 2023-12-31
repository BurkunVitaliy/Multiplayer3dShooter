using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonLaucher : MonoBehaviourPunCallbacks
{

    public static List<string> RoomList = new List<string>();

    public GameObject mainMenu , loading, roomMenu, joinRoom;
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        loading.SetActive(false);
        mainMenu.SetActive(true);
        Debug.Log("Connected to lobby");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connected to the Room");
        loading.SetActive(false);
        roomMenu.SetActive(true);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Not connected to the Room" + message);
    }

    public override void ChangeRooms(List<RoomInfo> roomList)
    {
        RoomList.Clear();
        foreach (var el in roomList)
        {
            RoomList.Add(el.Name);
        }
        joinRoom.GetComponent<ListOfRooms>().ChangeRooms();
    }
}
