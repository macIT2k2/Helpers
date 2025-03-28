using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Watermelon.SquadShooter
{
    public class DynamicGridLayout : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup gridLayout;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private ContentSizeFitter contentSizeFitter;

        [SerializeField] private float baseCellSizeX = 119.2f;
        [SerializeField] private float baseCellSizeY = 136.78f;

        [SerializeField] private float baseSpacingX = 17.4f;
        [SerializeField] private float baseSpacingY = 17.6f;

        [SerializeField] private float ratio;

        //when rows are higher than 1 , edit the cell size and spacing
        [Sirenix.OdinInspector.Button]
        private void ResizeGrid()
        {
            // if (gridLayout.constraint == GridLayoutGroup.Constraint.FixedRowCount)
            // {
            //     gridLayout.cellSize = new Vector2(baseCellSizeX * ratio, baseCellSizeY * ratio);
            //     gridLayout.spacing = new Vector2(baseSpacingX * ratio, baseSpacingY * ratio);
            // }
            // else
            // {
            //     gridLayout.cellSize = new Vector2(baseCellSizeX * ratio, baseCellSizeY * ratio);
            //     gridLayout.spacing = new Vector2(baseSpacingX * ratio, baseSpacingY * ratio);
            // }
            //get current row of the grid
            int row = gridLayout.transform.childCount / gridLayout.constraintCount;
            //if row is higher than 1, edit the cell size and spacing
            if (row > 1)
            {
                gridLayout.cellSize = new Vector2(baseCellSizeX * ratio, baseCellSizeY * ratio);
                gridLayout.spacing = new Vector2(baseSpacingX * ratio, baseSpacingY * ratio);
            }
            else
            {
                gridLayout.cellSize = new Vector2(baseCellSizeX, baseCellSizeY);
                gridLayout.spacing = new Vector2(baseSpacingX, baseSpacingY);
            }
        }

        [Sirenix.OdinInspector.Button]
        private void ResetToBase()
        {
            gridLayout.cellSize = new Vector2(baseCellSizeX, baseCellSizeY);
            gridLayout.spacing = new Vector2(baseSpacingX, baseSpacingY);
        }
    }
}