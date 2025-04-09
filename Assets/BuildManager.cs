using UnityEngine;
public class BuildManager : MonoBehaviour{
    public static BuildManager main;
    [Header("References")]

    [SerializeField] private GameObject[] troopPrefabs ;

    private int selectedtroop = 0;
    private void Awake() {
        main = this;
    }   
   public GameObject GetSelectedTroop(){
        return troopPrefabs[selectedtroop];
   }     
}

