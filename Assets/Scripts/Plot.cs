using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private GameObject Troop;
    private Color startColor;

    // Adicionando a variável para verificar se o plot é colocável
    public bool isPlaceable = true; // Define se o plot é colocável ou não

    private Collider2D plotCollider; // Referência para o Collider2D

    private void Start()
    {
        startColor = sr.color;

        // Obter o Collider2D do plot
        plotCollider = GetComponent<Collider2D>();

        // Se o plot não for colocável, desabilitar o collider
        if (!isPlaceable && plotCollider != null)
        {
            plotCollider.enabled = false;
        }
    }

    private void OnMouseEnter()
    {
        // Só muda a cor de hover se o plot for colocável
        if (isPlaceable)
        {
            sr.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    // Removendo a funcionalidade de clique
    /*private void OnMouseDown()
    {
        // Verifica se o plot é colocável antes de permitir qualquer ação
        if (!isPlaceable)
        {
            Debug.Log("Este plot não é colocável!");
            return;
        }

        // Se já houver uma torre, não permite colocar outra
        if (Troop != null) return;

        Tower TroopToBuild = BuildManager.main.GetSelectedTroop();

        if (TroopToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("Você não tem moeda suficiente para colocar esta torre.");
            return;
        }

        LevelManager.main.SpendCurrency(TroopToBuild.cost);

        // Instancia a torre no plot
        Troop = Instantiate(TroopToBuild.prefab, transform.position, Quaternion.identity);
    }*/
}
