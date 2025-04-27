using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] PanelType panelType;
    [HideInInspector] public PanelType Type => panelType; //���ٽ� �� ����. public PanelType Type { get { return panelType; } } �̶� ���� ǥ��

    public void OpenPanel()
    {
        gameObject.SetActive(true);

        // å �ѱ� ����
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}

public enum PanelType { GameOver, Pause, Settings, ToolTips }
