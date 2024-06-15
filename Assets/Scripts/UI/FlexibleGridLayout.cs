using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FlexibleGridLayout : LayoutGroup
    {
        public enum FitType
        {
            Uniform,
            Width,
            Height,
            FixedRows,
            FixedColumns
        }

        [Header("Flexible Grid")] 
        public FitType fitType = FitType.Uniform;
        public int rows;
        public int columns;
        public Vector2 cellSize;
        public Vector2 spacing;

      
        public bool fitX;
        public bool fitY;
        public bool keepCellsSquare;
        
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            
            if (fitType is FitType.Width or FitType.Height or FitType.Uniform)
            {
                var squareRoot = Mathf.Sqrt(transform.childCount);
                rows = columns = Mathf.CeilToInt(squareRoot);
            }
            
            if (fitType is FitType.Width or FitType.FixedColumns)
            {
                rows = Mathf.CeilToInt(transform.childCount / (float)columns);
            }
            if (fitType is FitType.Height or FitType.FixedRows)
            {
                columns = Mathf.CeilToInt(transform.childCount / (float)rows);
            }
            
            var parentWidth = rectTransform.rect.width - padding.left - padding.right;
            var parentHeight = rectTransform.rect.height - padding.top - padding.bottom;
            
            var cellWidth = parentWidth / columns - spacing.x / columns * (columns - 1);
            var cellHeight = parentHeight / rows - spacing.y / rows * (rows - 1);
            
            cellSize.x = fitX ? cellWidth : cellSize.x;
            cellSize.y = fitY ? cellHeight : cellSize.y;
            
            if (keepCellsSquare)
                cellSize.y = cellSize.x;
            
            
            var totalWidth = cellSize.x * columns + spacing.x * (columns - 1);
            var totalHeight = cellSize.y * rows + spacing.y * (rows - 1);
            
            var extraWidth = rectTransform.rect.width - totalWidth - padding.left - padding.right;
            var extraHeight = rectTransform.rect.height - totalHeight - padding.top - padding.bottom;
            
            var startX = padding.left + extraWidth * ((int)childAlignment % 3) * 0.5f;
            var startY = padding.top + extraHeight * ((int)childAlignment / 3) * 0.5f;
            
            var columnCount = 0;
            var rowCount = 0;

            for (var i = 0; i < rectChildren.Count; i++)
            {
                rowCount = i / columns;
                columnCount = i % columns;

                var item = rectChildren[i];

                var xPos = startX + (cellSize.x + spacing.x) * columnCount;
                var yPos = startY + (cellSize.y + spacing.y) * rowCount;

                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }
            
            SetLayoutInputForAxis(0, totalWidth + padding.horizontal, 0, 0);
            SetLayoutInputForAxis(0, totalHeight + padding.vertical, 0, 1);
        }
        
        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
        }

        public override void SetLayoutVertical()
        {
        }
    }
}