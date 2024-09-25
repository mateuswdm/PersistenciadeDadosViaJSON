using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class xml : MonoBehaviour
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


        //salvar
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("O jogo foi salvo");
            gravar();

           
        }


        //carregar | ler 
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("O jogo foi carregado");
            ler();
        }
    }

    public void gravar()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "dados.xml");

        Cordinates cordinates = new Cordinates();

        cordinates.xValue = this.gameObject.transform.position.x;
        cordinates.yValue = this.gameObject.transform.position.y;
        cordinates.zValue = this.gameObject.transform.position.z;



        print(filePath);

        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        if (!File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Cordinates));
            StreamWriter writer = new StreamWriter(filePath);
            serializer.Serialize(writer.BaseStream, cordinates);
            writer.Close();
        }
        else
        {
            File.Delete(Application.streamingAssetsPath + "/dados.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(Cordinates));
            StreamWriter writer = new StreamWriter(filePath);
            serializer.Serialize(writer.BaseStream, cordinates);
            writer.Close();
        }
    }

    public void ler()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "dados.xml");

        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Cordinates));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Cordinates cordinates = (Cordinates)serializer.Deserialize(reader.BaseStream);
                // Agora você pode usar os valores de 'cordinates' como quiser
                Debug.Log("X: " + cordinates.xValue);
                Debug.Log("Y: " + cordinates.yValue);
                Debug.Log("Z: " + cordinates.zValue);

                

                transform.position = new Vector3(cordinates.xValue, cordinates.yValue, cordinates.zValue);


            }
        }
        else
        {
            Debug.LogWarning("Arquivo não encontrado!");
        }

    }

    public void apagar()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "dados.xml");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        else
        {
            Debug.LogWarning("Arquivo já não existe ou não foi encontrado.");
        }
    }
}
