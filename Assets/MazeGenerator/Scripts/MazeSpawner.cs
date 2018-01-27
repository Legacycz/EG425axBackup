using UnityEngine;
using System.Collections;
using UnityEditor;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour
{
    public enum MazeGenerationAlgorithm
    {
        PureRecursive,
        RecursiveTree,
        RandomTree,
        OldestTree,
        RecursiveDivision,
    }

    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom = false;
    public int RandomSeed = 12345;
    public GameObject Floor = null;
    public GameObject Wall = null;
    public GameObject Pillar = null;
    public int Rows = 5;
    public int Columns = 5;
    public float CellWidth = 5;
    public float CellHeight = 5;
    public bool AddGaps = true;
    public GameObject GoalPrefab = null;

    private BasicMazeGenerator mMazeGenerator = null;

    [ContextMenu("GenerateMaze")]
    public void GenerateMaze()
    {
        ClearMaze();
        float heightOffset = transform.position.y;

        if (!FullRandom)
        {
            Random.InitState(RandomSeed)    ;
        }
        switch (Algorithm)
        {
            case MazeGenerationAlgorithm.PureRecursive:
                mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveTree:
                mMazeGenerator = new RecursiveTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RandomTree:
                mMazeGenerator = new RandomTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.OldestTree:
                mMazeGenerator = new OldestTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveDivision:
                mMazeGenerator = new DivisionMazeGenerator(Rows, Columns);
                break;
        }
        mMazeGenerator.GenerateMaze();
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? .2f : 0));
                float z = row * (CellHeight + (AddGaps ? .2f : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp = (GameObject)PrefabUtility.InstantiatePrefab(Floor);
                tmp.transform.position = new Vector3(x, heightOffset, z);
                tmp.transform.rotation = Quaternion.Euler(0, 0, 0);
                tmp.transform.parent = transform;
                if (cell.WallRight)
                {
                    tmp = (GameObject)PrefabUtility.InstantiatePrefab(Wall);
                    tmp.transform.position = new Vector3(x + CellWidth / 2, heightOffset, z) + Wall.transform.position;
                    tmp.transform.rotation = Quaternion.Euler(0, 90, 0);// right
                    tmp.transform.parent = transform;
                }
                if (cell.WallFront)
                {
                    tmp = (GameObject)PrefabUtility.InstantiatePrefab(Wall);
                    tmp.transform.position = new Vector3(x, heightOffset, z + CellHeight / 2) + Wall.transform.position;
                    tmp.transform.rotation = Quaternion.Euler(0, 0, 0);// front
                    tmp.transform.parent = transform;
                }
                if (cell.WallLeft)
                {
                    tmp = (GameObject)PrefabUtility.InstantiatePrefab(Wall);
                    tmp.transform.position = new Vector3(x - CellWidth / 2, heightOffset, z) + Wall.transform.position;
                    tmp.transform.rotation = Quaternion.Euler(0, 270, 0);// left
                    tmp.transform.parent = transform;
                }
                if (cell.WallBack)
                {
                    tmp = (GameObject)PrefabUtility.InstantiatePrefab(Wall);
                    tmp.transform.position = new Vector3(x, heightOffset, z - CellHeight / 2) + Wall.transform.position;
                    tmp.transform.rotation = Quaternion.Euler(0, 180, 0);// back
                    tmp.transform.parent = transform;
                }
                if (cell.IsGoal && GoalPrefab != null)
                {
                    tmp = (GameObject)PrefabUtility.InstantiatePrefab(GoalPrefab);
                    tmp.transform.position = new Vector3(x, heightOffset + 1, z);
                    tmp.transform.rotation = Quaternion.Euler(0, 0, 0);
                    tmp.transform.parent = transform;
                }
            }
        }
        if (Pillar != null)
        {
            for (int row = 0; row < Rows + 1; row++)
            {
                for (int column = 0; column < Columns + 1; column++)
                {
                    float x = column * (CellWidth + (AddGaps ? .2f : 0));
                    float z = row * (CellHeight + (AddGaps ? .2f : 0));
                    GameObject tmp = (GameObject)PrefabUtility.InstantiatePrefab(Pillar);
                    tmp.transform.position = new Vector3(x - CellWidth / 2, heightOffset, z - CellHeight / 2);
                    tmp.transform.rotation = Quaternion.identity;
                    tmp.transform.parent = transform;
                }
            }
        }
    }

    [ContextMenu("ClearMaze")]
    public void ClearMaze()
    {
        Transform[] childTransforms = transform.GetComponentsInChildren<Transform>();

        for (int i = childTransforms.Length - 1; i >= 0; --i)
        {
            if (childTransforms[i] != transform)
            {
                DestroyImmediate(childTransforms[i].gameObject);
            }
        }
    }
}