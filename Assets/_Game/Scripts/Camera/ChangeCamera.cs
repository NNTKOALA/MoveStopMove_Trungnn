using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] private GameObject _cameraMenu;
    [SerializeField] private GameObject _cameraFollow;
    // Start is called before the first frame update
    void Start()
    {
        _cameraMenu.SetActive(true);
        _cameraFollow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCameraInGame()
    {
        _cameraMenu.SetActive(false);
        _cameraFollow.SetActive(true);
    }

    public void ChangeCameraGameMenu()
    {
        _cameraMenu.SetActive(true);
        _cameraFollow.SetActive(false);
    }
}
