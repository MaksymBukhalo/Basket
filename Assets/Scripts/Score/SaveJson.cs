using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveJson : MonoBehaviour
{
    private Save save = new Save();
    private string path;

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        path = Path.Combine(Application.dataPath, "Save.json");
#endif
        if (File.Exists(path))
        {
            save = JsonUtility.FromJson<Save>(File.ReadAllText(path));
        }
        else
        {
            save.scoreOfNumber = 0;
        }
    }

    public void CheckScore(int score)
    {
        if (score > save.scoreOfNumber)
        {
            save.scoreOfNumber = score;
            File.WriteAllText(path, JsonUtility.ToJson(save));
        }
    }

    public int GetScore()
    {
        return save.scoreOfNumber;
    }

	private void OnApplicationQuit()
	{
        File.WriteAllText(path, JsonUtility.ToJson(save));
    }

    [SerializeField]
    private class Save
    {
        public int scoreOfNumber;
    }
}
