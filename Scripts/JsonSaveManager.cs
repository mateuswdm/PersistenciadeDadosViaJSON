using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSaveManager : MonoBehaviour
{
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Captura a entrada do teclado para movimentação horizontal e vertical
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcula o movimento com base na entrada e na velocidade
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;

        // Aplica o movimento ao objeto
        transform.Translate(movement);

        // Salvar
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("O jogo foi salvo");
            Salvar();
        }

        // Carregar | ler
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("O jogo foi carregado");
            Carregar();
        }
    }

    public void Salvar()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "dados.json");

        Cordinates cordinates = new Cordinates
        {
            xValue = this.gameObject.transform.position.x,
            yValue = this.gameObject.transform.position.y,
            zValue = this.gameObject.transform.position.z
        };

        print(filePath);

        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        string jsonData = JsonUtility.ToJson(cordinates, true); // Serializa o objeto em JSON

        File.WriteAllText(filePath, jsonData); // Escreve o arquivo JSON
    }

    public void Carregar()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "dados.json");

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath); // Lê o arquivo JSON
            Cordinates cordinates = JsonUtility.FromJson<Cordinates>(jsonData); // Desserializa os dados do JSON

            Debug.Log("X: " + cordinates.xValue);
            Debug.Log("Y: " + cordinates.yValue);
            Debug.Log("Z: " + cordinates.zValue);

            transform.position = new Vector3(cordinates.xValue, cordinates.yValue, cordinates.zValue); // Atualiza a posição
        }
        else
        {
            Debug.LogWarning("Arquivo não encontrado!");
        }
    }

    public void Apagar()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "dados.json");

        if (File.Exists(filePath))
        {
            File.Delete(filePath); // Deleta o arquivo JSON
        }
        else
        {
            Debug.LogWarning("Arquivo já não existe ou não foi encontrado.");
        }
    }
}

[System.Serializable]
public class Cordinates
{
    public float xValue;
    public float yValue;
    public float zValue;
}
