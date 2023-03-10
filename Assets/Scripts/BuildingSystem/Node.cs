using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    private Renderer rend;
    private Material startMaterial;
    public Material hoverMaterial;
    public Material cantBuildMaterial;


    private GameObject unit;
    private UnitBlueprint blueprint;
    public float offset; // Kiek pakelti
    private BuildManager buildmanager;

    [SerializeField] private int groundType;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        startMaterial = rend.material;
        buildmanager = BuildManager.instance;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //Building turret:
        if (unit != null)
        {
            //Reiskias turim lankininka ant sito tile
            return;
        }

        //Instantiate archer.
        buildUnit(buildmanager.getCurrentBlueprint(), buildmanager.currentSelectedUnit);
    }

    void buildUnit(UnitBlueprint blueprint, int index)
    {
        if (blueprint == null)
            return;
        if (!CanBuild(index))
            return;
        if (PlayerStats.Money < blueprint.cost)
            return;

        PlayerStats.Money -= blueprint.cost;
        unit = Instantiate(blueprint.prefab,
            new Vector3(transform.position.x, transform.position.y + offset, transform.position.z),
            Quaternion.identity);
    }

    private bool CanBuild(int index)
    {
        if (groundType == 0 && index == 3)
            return false;
        if (groundType == 1 && (index == 0 || index == 1 || index == 2))
            return false;
        return true;
    }

    private void OnMouseEnter()
    {
        if (!buildmanager.buildingMode)
            return;
        if (CanBuild(buildmanager.currentSelectedUnit) && buildmanager.HasMoney)
        {
            rend.material = hoverMaterial;
        }
        else
        {
            rend.material = cantBuildMaterial;
        }
    }

    private void OnMouseExit()
    {
        if (!buildmanager.buildingMode)
            return;
        rend.material = startMaterial;
    }
}