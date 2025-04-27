using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] PanelType panelType;
    [HideInInspector] public PanelType Type => panelType; //람다식 좀 쓰자. public PanelType Type { get { return panelType; } } 이랑 같은 표현

    public void OpenPanel()
    {
        gameObject.SetActive(true);

        // 책 넘김 사운드
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}

public enum PanelType { GameOver, Pause, Settings, ToolTips }
