using System;
using UnityEngine.UIElements;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    private Button _createRoomButton, _joinroomButton;
    public GameObject createRoom, joinRoom;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _createRoomButton = root.Q<Button>("CreateRoomButton");
        _createRoomButton.clicked += CreateRoomButtonOnClicked;
        
        _joinroomButton = root.Q<Button>("JoinRoomButton");
        _joinroomButton.clicked += JoinRoomButtonOnClicked;
    }

    private void JoinRoomButtonOnClicked()
    {
        gameObject.SetActive(false);
        joinRoom.SetActive(true);
    }

    private void CreateRoomButtonOnClicked()
    {
        gameObject.SetActive(false);
        createRoom.SetActive(true);
    }
}
