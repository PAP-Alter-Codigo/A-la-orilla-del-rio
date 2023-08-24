using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{

    public string saveFile;

    public Data data = new Data();

    private void Awake()
    {
        saveFile = Application.dataPath + "/saveFile.json";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CargarDatos();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardarDatos();
        }
    }

    void CargarDatos()
    {
        if (File.Exists(saveFile))
        {

            string contenido = File.ReadAllText(saveFile);
            data = JsonUtility.FromJson<Data>(contenido);
            SceneManager.LoadScene(data.sceneName);
        }
        else
        {
            Debug.Log("The file does not exists");
        }
    }

    void GuardarDatos()
    {
		Scene scene = SceneManager.GetActiveScene();
		
        Data newData = new Data()
        {
            sceneName = scene.name,
			collectibles = new HashSet<string>()
        };

        string cadenaJSON = JsonUtility.ToJson(newData);
        File.WriteAllText(saveFile, cadenaJSON);
        Debug.Log("File Saved");
    }
}
