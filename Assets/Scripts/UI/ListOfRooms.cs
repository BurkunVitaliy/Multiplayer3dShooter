using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UIElements;

public class ListOfRooms : MonoBehaviourPunCallbacks
{
    public GameObject loadingUI;

    private VisualElement _listOgRooms;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _listOgRooms = root.Q<VisualElement>("ListOfRooms");
        ChangeRooms();
    }

    public  void ChangeRooms()
    {
        if (_listOgRooms == null) return;
        
        _listOgRooms.Clear();
        foreach (string el in PhotonLaucher.RoomList)
        {
            Button button = new Button();
            button.text = el;
            button.clicked += () => JoinRoom(el);
            _listOgRooms.Add(button);
        }
    }

    private void JoinRoom(string name)
    {
        PhotonNetwork.JoinRoom(name);
        loadingUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
