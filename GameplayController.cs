using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviourPunCallbacks
{
    public GameObject panelYouWin;
    public GameObject panelYouLose;

    [SerializeField] GameObject myPlayer;
    public List<Transform> spawnPoints;

    void Start()
    {
        int i = Random.Range(0, spawnPoints.Count);

        PhotonNetwork.Instantiate(myPlayer.name, spawnPoints[i].position, spawnPoints[i].rotation, 0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("SceneLobby");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount <= 1)
        {
            LeaveRoom();
        }
    }
}
