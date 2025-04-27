using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] PanelType panelType;
    GameManager gm;
    void Start()
    {
        gm = GameManager.Instance;
        switch (panelType)
        {
            case PanelType.GameOver:
                gm.GameOverEvent.AddListener(OpenPanel);
                break;

            case PanelType.Pause:
                gm.PauseEvent.AddListener(OpenPanel);
                break;

            case PanelType.Settings:
            default:
                Debug.LogWarning("Ÿ���� �������� ���� �г��� ����");
                break;
        }

        gameObject.SetActive(false);
    }

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

public enum PanelType { GameOver, Pause, Settings }
