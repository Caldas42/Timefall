using UnityEngine;
public class BuildManager : MonoBehaviour{
    public static BuildManager main;
    
    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int selectedtroop = 0;

    private void Awake() {
        main = this; 
    }

   public Tower GetSelectedTroop(){
        return towers[selectedtroop];
   }  

   public void SetSelectedTower(int _selectedTower) {
        selectedtroop = _selectedTower;
   } 
}

