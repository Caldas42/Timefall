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

    GameObject TroopToBuild = BuildManager.main.GetSelectedTroop();
    Troop = Instantiate(TroopToBuild, transform.position, Quaternion.identity);
    }
}
