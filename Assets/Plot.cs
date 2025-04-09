using UnityEngine;

public class Plot : MonoBehaviour{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr; 
    [SerializeField] private Color hoverColor;
    private GameObject Troop;
    private Color startColor;

    private void Start(){
        startColor = sr.color;
    } 
    private void OnMouseEnter(){
       sr.color =  hoverColor;
    }
    private void OnMouseExit(){
       sr.color = startColor;
    }
    private void OnMouseDown(){
      if(Troop != null) return;

    Tower TroopToBuild = BuildManager.main.GetSelectedTroop();

    if(TroopToBuild.cost > LevelManager.main.currency){
      Debug.Log("YOu can't afford this tower");
      return;
    }

    LevelManager.main.SpendCurrency(TroopToBuild.cost);

    Troop = Instantiate(TroopToBuild.prefab, transform.position, Quaternion.identity);
    }
}
