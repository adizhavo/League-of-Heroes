using UnityEngine;
using System.Collections.Generic;

public class DeployableCells : MonoBehaviour {

    private static DeployableCells instance;
    public static DeployableCells Instance { get { return instance; } } 

    [SerializeField] private int DeployableIdY;
    private List<GridCell> deployableCell = new List<GridCell>();

    private void Start()
    {
        instance = this;

        for (int y = 0; y < DeployableIdY; y ++)
            for (int x = 0; x < Grid.Instance.XSize; x ++)
                if(Grid.Instance.IsCellInGrid(x, y))
                {
                    GridCell cell = Grid.Instance.GetCell(x, y);
                    cell.CanDeploy = true;
                    deployableCell.Add(cell);
                }
    }

    public void HighLight()
    {
        for (int i = 0; i < deployableCell.Count; i ++)
            SetAlphaValue(deployableCell[i].transform, 0.8f);
    }

    public void Release()
    {
        for (int i = 0; i < deployableCell.Count; i ++)
            SetAlphaValue(deployableCell[i].transform, 0.5f);
    }

    private void SetAlphaValue(Transform alphaTr, float alphaValue)
    {
        SetAlphaToTr(alphaTr.GetComponent<SpriteRenderer>(), alphaValue);

        for (int i = 0; i < alphaTr.childCount; i++)
        {
            SetAlphaValue(alphaTr.GetChild(i), alphaValue);
        }
    }

    private void SetAlphaToTr(SpriteRenderer GraphicElement, float alphaValue)
    {
        if (GraphicElement == null)
            return;

        Color objectColor = GraphicElement.color;
        objectColor.a = alphaValue;
        GraphicElement.color = objectColor;
    }
}
