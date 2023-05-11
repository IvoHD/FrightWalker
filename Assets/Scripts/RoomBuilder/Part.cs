using UnityEngine;
using System.Collections.Generic;

public class Part : MonoBehaviour
{
    RoomStyle Style;
    RoomManager RM;

    public void Initiate(RoomManager roomManager, RoomStyle style)
    {
        RM = roomManager;
        Style = style;
        PartPlanner();
    }

    private void PartPlanner()
    {
        RM.RoomParts.Add(RectGen(Style.RoomSize));
        for (int i = 0; i < Style.Parts; i++)
        {
            RM.RoomParts.Add(RectGen(Style.RoomSize/2));
        }
    }

    private GameObject[,] RectGen(int n)
    {
        int x = MathsRand.Instance.RandNumOutOfRange(-1, n % 3 + 1);
        int z = MathsRand.Instance.RandNumOutOfRange(-1, n % 3 + 1);
        return new GameObject[n + x, n + z];
    }
}
