using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHealth : MonoBehaviour
{
    [Header("Gameplay Controller")]
    GameplayController gameplayController;

    [Header("Network")]
    PhotonView photonView;

    [Header("Player Behaviours")]
    PlayerMovement playerMovement;
    SpawnBomb spawnBomb;

    bool isDead = false;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        playerMovement = GetComponent<PlayerMovement>();
        spawnBomb = GetComponent<SpawnBomb>();

        gameplayController = GameObject.Find("GameplayController").GetComponent<GameplayController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        photonView.RPC("TakeDamageNetwork", RpcTarget.All);
    }

    [PunRPC]
    public void TakeDamageNetwork()
    {
        Death();
    }

    public void Death()
    {
        if (photonView.IsMine && !isDead)
        {
            playerMovement.enabled = false;
            spawnBomb.enabled = false;

            gameplayController.panelYouLose.SetActive(true);

            gameplayController.LeaveRoom();

            isDead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))
        {
            if (photonView.IsMine)
            {
                Debug.Log("Game Over" + photonView.Owner.NickName);
                TakeDamage();
            }
        }
    }

}
