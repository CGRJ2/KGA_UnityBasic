using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<Panel> panels = new List<Panel>();

    private GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;

        foreach (var panel in panels)
        {
            switch (panel.Type)
            {
                case PanelType.GameOver:
                    if (panel is Panel_GameOver panel_GameOver)
                    gm.GameOverEvent.AddListener(panel_GameOver.GameOverPanelActive);
                    break;
                case PanelType.Pause:
                    gm.PauseEvent.AddListener(panel.OpenPanel);
                    break;
                case PanelType.Settings:
                // 필요하면 추가
                default:
                    Debug.LogWarning("타입이 지정되지 않은 패널이 있음");
                    break;
            }
            panel.gameObject.SetActive(false); // 항상 꺼둔 상태로
        }
    }




    
}