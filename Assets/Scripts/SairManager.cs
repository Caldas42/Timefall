using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel;      // Painel de confirmação
    public Button botaoAbrirPopup;     // Botão "SAIR"
    public Button botaoSim;
    public Button botaoNao;

    void Start()
    {
        popupPanel.SetActive(false);

        botaoAbrirPopup.onClick.AddListener(() =>
        {
            popupPanel.SetActive(true);
        });

        botaoSim.onClick.AddListener(() =>
        {
            Debug.Log("Encerrando o jogo...");
            Application.Quit();
        });

        botaoNao.onClick.AddListener(() =>
        {
            Debug.Log("Cancelou a saída");
            popupPanel.SetActive(false);
        });
    }
}
