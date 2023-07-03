
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomMenu : MonoBehaviour
{
    private Button _startGameButton;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        Label roomName = root.Q<Label>("roomName");
        roomName.text = "Комната создана: " + PhotonNetwork.CurrentRoom.Name;

        _startGameButton = root.Q<Button>("StartGameButton");
        _startGameButton.clicked += StartGameButtonOnclicked;

        if (!PhotonNetwork.IsMasterClient)
        {
            _startGameButton.SetEnabled(false);
        }
    }

    private void StartGameButtonOnclicked()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
