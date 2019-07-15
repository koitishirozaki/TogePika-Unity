using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    private playerController playerController;
    private GameObject player;
    private CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<playerController>();
        vcam = this.gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.grounded)
        {
            vcam.m_Lens.OrthographicSize = 5f;
            Debug.Log("camera should zoom out");
        }
        else
            vcam.m_Lens.OrthographicSize = 3.2f;
    }
}
