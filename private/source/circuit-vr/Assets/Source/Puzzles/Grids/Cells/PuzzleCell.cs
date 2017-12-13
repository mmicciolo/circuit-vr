using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Puzzles.Grids.Cells
{
    class PuzzleCell : MonoBehaviour
    {

        private List<GameObject> lines = new List<GameObject>();
        private Vector2 gridPosition;

        private void Start()
        {
            
        }

        public void DrawCell(float height, float width, Material material)
        {
            //Draw everything for the first time
            if(lines.Count == 0)
            {
                //Draw the top line
                DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(width, 0, 0), "top", material);

                //Draw the bottom line
                DrawLine(gameObject.transform.position - new Vector3(0, height, 0), gameObject.transform.position + new Vector3(width, -height, 0), "bottom", material);

                //Draw the left line
                DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(0, -height, 0), "left", material);

                //Draw the right line
                DrawLine(gameObject.transform.position + new Vector3(width, 0, 0), gameObject.transform.position + new Vector3(width, -height, 0), "right", material);
            }
            else
            {
                //Update position
                for(int i = 0; i < lines.Count(); i++)
                {
                    if(lines[i].name.Equals("top"))
                    {
                        
                    }
                }
            }
        }

        private void DrawLine(Vector3 start, Vector3 end, String name, Material material)
        {
            GameObject line = new GameObject(name);
            line.AddComponent<LineRenderer>();
            LineRenderer lr = line.GetComponent<LineRenderer>();
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
            lr.material = material;
            line.transform.parent = gameObject.transform;
            lines.Add(line);
        }

        public Vector2 GridPosition
        {
            get { return gridPosition; } 
            set { gridPosition = value; }
        }
    }
}
