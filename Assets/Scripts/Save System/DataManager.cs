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
		// We make sure we have a save file
        if (File.Exists(saveFile))
        {
			// We read the file content
            string base64Encoded = File.ReadAllText(saveFile);

			// We base64 decode it
			var jsonBytes = System.Convert.FromBase64String(base64Encoded);

			string JSONData = System.Text.Encoding.UTF8.GetString(jsonBytes);

			// We turn it back from json to a Data object
            data = JsonUtility.FromJson<Data>(JSONData);
            SceneManager.LoadScene(data.sceneName);
        }
        else
        {
            Debug.Log("The file does not exists");
        }
    }

    void GuardarDatos()
    {
		// We get the data we are going to save
		Scene scene = SceneManager.GetActiveScene();
		
		// We create the Data instance
        Data newData = new Data()
        {
            sceneName = scene.name,
			collectibles = new HashSet<string>()
        };

		// We encode it to Json 
        string cadenaJSON = JsonUtility.ToJson(newData);
		
		// And on base64
		var jsonBytes = System.Text.Encoding.UTF8.GetBytes(cadenaJSON);
		string base64Encoded = System.Convert.ToBase64String(jsonBytes);

		// And write it to file
        File.WriteAllText(saveFile, base64Encoded);
        Debug.Log("File Saved");
    }
}
