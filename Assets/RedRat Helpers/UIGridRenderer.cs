using UnityEngine;
using UnityEngine.UI;

namespace RedRats.Core
{
    /// <summary>
    /// Renders a UI grid.
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    public class UIGridRenderer : MaskableGraphic
    {
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private float thickness;

        private float width, height;
        private float cellWidth;
        private float cellHeight;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            width = rectTransform.rect.width;
            height = rectTransform.rect.height;

            cellWidth = width / gridSize.x;
            cellHeight = height / gridSize.y;

            int count = 0;
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    DrawGridCell(x, y, count, vh);
                    count++;
                }
            }
        }

        /// <summary>
        /// Draws a singular cell in a grid.
        /// </summary>
        /// <param name="x">X Position</param>
        /// <param name="y">Y Position</param>
        /// <param name="index">Index of cell</param>
        /// <param name="vh">Vertex Helper</param>
        private void DrawGridCell(int x, int y, int index, VertexHelper vh)
        {
            float posX = cellWidth * x - (width * 0.5f);
            float posY = cellHeight * y - (height * 0.5f);

            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;

            vertex.position = new Vector3(posX, posY);
            vh.AddVert(vertex);
            vertex.position = new Vector3(posX, posY + cellHeight);
            vh.AddVert(vertex);
            vertex.position = new Vector3(posX + cellWidth, posY + cellHeight);
            vh.AddVert(vertex);
            vertex.position = new Vector3(posX + cellWidth, posY);
            vh.AddVert(vertex);

            float widthSqr = thickness * thickness;
            float distanceSqr = widthSqr / 2f;
            float distance = Mathf.Sqrt(distanceSqr);

            vertex.position = new Vector3(posX + distance, posY + distance);
            vh.AddVert(vertex);
            vertex.position = new Vector3(posX + distance, posY + (cellHeight - distance));
            vh.AddVert(vertex);
            vertex.position = new Vector3(posX + (cellWidth - distance), posY + (cellHeight - distance));
            vh.AddVert(vertex);
            vertex.position = new Vector3(posX + (cellWidth - distance), posY + distance);
            vh.AddVert(vertex);

            int offset = index * 8;

            vh.AddTriangle(offset + 0, offset + 1, offset + 5);
            vh.AddTriangle(offset + 5, offset + 4, offset + 0);
            vh.AddTriangle(offset + 1, offset + 2, offset + 6);
            vh.AddTriangle(offset + 6, offset + 5, offset + 1);
            vh.AddTriangle(offset + 2, offset + 3, offset + 7);
            vh.AddTriangle(offset + 7, offset + 6, offset + 2);
            vh.AddTriangle(offset + 3, offset + 0, offset + 4);
            vh.AddTriangle(offset + 4, offset + 7, offset + 3);
        }

        public Vector2Int GridSize { get => gridSize; }
        public float Width { get => width;}
        public float Height { get => height;}
        public float CellWidth { get => cellWidth;}
        public float CellHeight { get => cellHeight;}
        public float Thickness { get => thickness; }
    }
}