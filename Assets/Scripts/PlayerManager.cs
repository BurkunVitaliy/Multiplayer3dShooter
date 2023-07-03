using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{
    
    private PhotonView _photonView;
    private Vector3[] _spawnPosition = new Vector3[4];
    private void Start()
    {
        _spawnPosition[0] = new Vector3(1,0,-7);
        _spawnPosition[1] = new Vector3(10,0,14);
        _spawnPosition[2] = new Vector3(-10,0,19);
        _spawnPosition[3] = new Vector3(13,4.25f,27);
        
        _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine)
            CreatePlayer();
    }

    private void CreatePlayer()
    {
        Vector3 randomCoordinate = GetRandomCoordinate();
        PhotonNetwork.Instantiate("Player", randomCoordinate, Quaternion.identity);
    }
    
    private Vector3 GetRandomCoordinate()
    {
        int randomIndex = Random.Range(0, _spawnPosition.Length);
        return _spawnPosition[randomIndex];
    }
  

}
