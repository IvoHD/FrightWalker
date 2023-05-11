
/*
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "New TeacherModule", menuName = "Modules/TeacherModule")]


public class TeacherModule : ScriptableObject, IModule
{
    [field: SerializeField]
    Material moduleMaterial;
    GameObject[,] area;
    public void Setup(GameObject[,] part)
    {
        area = part;
    }
    public void Deploy()
    {


        int[] corner = new int[2] { MathsRand.Chance(2) ? area.GetLength(0) - 1 : 0, MathsRand.Chance(2) ? area.GetLength(1) - 1 : 0 };

        bool directionOfTeacherModule = MathsRand.Chance(2);

        float teacherModuleLenght = directionOfTeacherModule ? Mathf.Round(area.GetLength(0) / 3) : Mathf.Round(area.GetLength(1) / 3);


        for (int i = 0; i < teacherModuleLenght; i++)
        {
            int x = corner[0];
            int y = corner[1];
            if (directionOfTeacherModule)
            {
                x += (corner[0] > 0) ? -i : i;
            }
            else
            {
                y += (corner[1] > 0) ? -i : i;
            }
            area[x, y].transform.tag = "TeacherModule";
        }
    }
    public void Mark()
    {
        GameObject[] teacherSpace = GameObject.FindGameObjectsWithTag("TeacherModule");
        foreach (GameObject slot in teacherSpace)
        {
            for (int i = 0; i < slot.transform.childCount; i++)
            {
                if (slot.transform.GetChild(i).name == "CenterPiece")
                {
                    slot.transform.GetChild(i).transform.GetComponent<MeshRenderer>().material = moduleMaterial;
                }
            }
        }
    }
}
*/
/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WorkplaceModule", menuName = "Modules/WorkplaceModule")]

public class WorkplaceModule : ScriptableObject, IModule
{
    [field: SerializeField]
    Material moduleMaterial;
    GameObject[,] area;
    public void Setup(GameObject[,] part)
    {
        area = part;
    }

    public void Deploy()
    {
        GameObject[,] workplaceArea = new GameObject[area.GetLength(0) - 2, area.GetLength(1)];
        for (int i = 2; i < area.GetLength(0); i++)
            for (int y = 0; y < area.GetLength(1); y++)
            {
                area[i, y].transform.tag = "WorkplaceModule";
                workplaceArea[i - 2, y] = area[i, y];
            }
        MoveSeatsPlan(workplaceArea);
    }
    public void MoveSeatsPlan(GameObject[,] workplaceArea)
    {
        for (int i = 0; i < 2; i++)
        {
            switch (MathsRand.RandNumOutOfRange(1, 3))
            {
                case 2:
                    MoveSeatsExecution(i, 0, workplaceArea);
                    break;
                case 3:
                    MoveSeatsExecution(i, workplaceArea.GetLength(i), workplaceArea);
                    break;
                default:
                    break;
            }

            Debug.Log("i: " + i);
        }

    }
    public void MoveSeatsExecution(int angle, int row, GameObject[,] workplaceArea)
    {
        for (int i = 0; i < workplaceArea.GetLength(angle); i++)
        {
            if (angle == 0)
                workplaceArea[i, row].transform.tag = "GroundObjects";
            else
                workplaceArea[row, i].transform.tag = "GroundObjects";
        }
    }

    public void Mark()
    {
        GameObject[] moduleSpace = GameObject.FindGameObjectsWithTag("WorkplaceModule");
        foreach (GameObject slot in moduleSpace)
        {
            for (int i = 0; i < slot.transform.childCount; i++)
            {
                if (slot.transform.GetChild(i).name == "CenterPiece")
                {
                    slot.transform.GetChild(i).transform.GetComponent<MeshRenderer>().material = moduleMaterial;
                }
            }
        }
    }
}
*/
