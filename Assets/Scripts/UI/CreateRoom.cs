using Photon.Pun;
using UnityEngine.UIElements;
using UnityEngine;

public class CreateRoom : MonoBehaviour
{
    private Button _createRoomButton;

    private TextField _userRoomName;

    public GameObject loadingUI;
    /*public GameObject createRoom;*/

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _createRoomButton = root.Q<Button>("CreateRoomButton");
        _createRoomButton.clicked += CreateRoomButtonOnClicked;

        _userRoomName = root.Q<TextField>("UserRoomName");
    }

    private void CreateRoomButtonOnClicked()
    {
       
        string userInput = _userRoomName.value;
        if (string.IsNullOrEmpty(userInput)) return;

        PhotonNetwork.CreateRoom(userInput);
        gameObject.SetActive(false);
        loadingUI.SetActive(true);
    }
}
