using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class NameMultiplayer : MonoBehaviour
{
    public Text playerNameText;
    public GameObject playerCanvas;

    PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        playerNameText.text = photonView.Owner.NickName;
    }

    private void Update()
    {
        playerCanvas.transform.LookAt(Camera.main.transform);
    }
}
