using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Realtime;
using Photon.Pun;

public class CinemachineControllerNetwork : MonoBehaviour
{
    CinemachineFreeLook cinemachineFreeLook;
    PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        cinemachineFreeLook = GameObject.Find("CinemachineVM").GetComponent<CinemachineFreeLook>();

        cinemachineFreeLook.Follow = this.transform;
        cinemachineFreeLook.LookAt = this.transform;
    }
}