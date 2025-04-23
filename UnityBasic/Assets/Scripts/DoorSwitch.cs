using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] GameObject door;
    bool isOpened = false;
    public void Open()
    {
        isOpened = true;
        door.transform.position += Vector3.down * 2;
    }

    public void Close()
    {
        isOpened = false;
        door.transform.position += Vector3.up * 2;
    }

    public void PressBtn()
    {
        if (isOpened) Close();
        else Open();
    }
}
