using UnityEditor;
using UnityEngine;

public class ScriptableEditor : EditorWindow
{
    const int gridSize = 10;
    bool[,] gridCells = new bool[gridSize, gridSize];
    public Scriptable gridData;

    [MenuItem("Window/Grid")]
    public static void ShowWindow()
    {
        GetWindow<ScriptableEditor>("2D 10*10 Grid");
    }

    private void OnEnable()
    {
        LoadGridFromData(); 
    }

    private void LoadGridFromData()
    {
        for (int row = 0; row < gridSize; row++)
        {
            for (int col = 0; col < gridSize; col++)
            {
                // updating grid cell values from gridData.
                gridCells[row, col] = gridData.GetValue(row * gridSize + col);
            }
        }
    }

    void OnGUI()
    {
        gridData = (Scriptable)EditorGUILayout.ObjectField("Grid Data", gridData, typeof(Scriptable), false);

        if (gridData == null)
        {
            EditorGUILayout.HelpBox("No Grid Data here", MessageType.Warning);
            if (GUILayout.Button("make new grid."))
            {
                CreateNewGridData();
            }
            return;
        }

        LoadGridFromData();

        GUILayout.Label("10x10 Grid", EditorStyles.boldLabel);

        for (int row = 0; row < gridSize; row++)
        {
            GUILayout.BeginHorizontal();
            for (int col = 0; col < gridSize; col++)
            {
                Color originalColor = GUI.backgroundColor;
                GUI.backgroundColor = gridCells[row, col] ? Color.green : Color.gray; // color =green if true , else gray

                if (GUILayout.Button("", GUILayout.Width(30), GUILayout.Height(30)))
                {
                    int index = row * gridSize + col;
                    Undo.RecordObject(gridData, "updating");
                    gridData.UpdateData(index);                     // updating Scriptable object ggrid values
                    EditorUtility.SetDirty(gridData);

                    gridCells[row, col] = gridData.GetValue(index); // Updating cur value grid values
                }

                GUI.backgroundColor = originalColor;
            }
            GUILayout.EndHorizontal();
        }
    }

    void CreateNewGridData()
    {
        var newGridData = CreateInstance<Scriptable>();
        newGridData.CreateData();

        string path = EditorUtility.SaveFilePanelInProject("Save Grid Data", "NewGridData.asset", "asset", "Save Grid Data");
        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(newGridData, path);
            AssetDatabase.SaveAssets();
            gridData = newGridData;
        }
    }
}