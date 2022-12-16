using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    public GameObject Cube;
    [SerializeField] private GameObject[] objects;
    Rigidbody rb;
    float hMov;
    float vMov;
    float timer, cubeTimer;
    public int sphereScore, capsuleScore;
    public float speed;
    public int objectDestroyed;
    public TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject GameOver;
    [SerializeField]
    private GameObject GameWon;
    [SerializeField]
    private TextMeshProUGUI levelText;
    public int score;

    bool level2Checked = false;
    bool level3Checked = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = 0;
        sphereScore = 1;
        capsuleScore = 2;
        levelText.text = "Level 1";
    }


    private void Update()
    {
        timer += Time.deltaTime;
        cubeTimer += Time.deltaTime;
        if (timer > 3f)
        {
            GameObject spawningObjects = Instantiate(objects[Random.Range(0, 2)], new Vector3(Random.Range(-4.5f, 4.5f), 0.35f, Random.Range(-4f, 4f)), Quaternion.identity);
            timer = 0;
        }               //for object instantiator
        if(cubeTimer>8f)
        {
            GameObject cube = Instantiate(Cube, new Vector3(Random.Range(-4.5f, 4.5f), 0.35f, Random.Range(-4f, 4f)), Quaternion.identity);
            cubeTimer = 0;
        }              //for cube instantiator
    }

    void FixedUpdate()
    {
        hMov = Input.GetAxis("Horizontal");
        vMov = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(hMov * speed, 0, vMov * speed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sphere"))
        {
            collision.gameObject.tag = "Untagged";
            Destroy(collision.gameObject, 1f);
            objectDestroyed++;
            score+=sphereScore;
            scoreText.text = "Score : "+score.ToString();
            if (level2Checked == false)
            {
                if (score > 100)
                {
                    sphereScore = 10;
                    capsuleScore = 12;
                    level2Checked = true;
                    levelText.text = "Level 2";
                }
            }
            if (level3Checked == false)
            {
                if (score > 200)
                {
                    sphereScore = 20;
                    capsuleScore = 22;
                    level3Checked = true;
                    levelText.text = "Level 3";
                }
            }
            if(score>=400)
            {
                score = 400;
                Time.timeScale = 0;
                GameWon.SetActive(true);
                SaveToJson();
            }                   //GameWon
        }

        if (collision.gameObject.CompareTag("Capsule"))
        {
            collision.gameObject.tag = "Untagged";
            Destroy(collision.gameObject, 1f);
            score+=capsuleScore;
            objectDestroyed++;
            scoreText.text = "Score : "+score.ToString();
            if (level2Checked == false)
            {
                if (score > 100)
                {
                    sphereScore = 10;
                    capsuleScore = 12;
                    level2Checked = true;
                    levelText.text = "Level 2";
                }
            }
            if (level3Checked == false)
            {
                if (score > 200)
                {
                    sphereScore = 20;
                    capsuleScore = 22;
                    level3Checked = true;
                    levelText.text = "Level 3";
                }
            }
            if (score >= 400)
            {
                score = 400;
                Time.timeScale = 0;
                GameWon.SetActive(true);
                SaveToJson();
            }                   //GameWon
        }

        if(collision.gameObject.CompareTag("Cube"))
        {
            Time.timeScale = 0;
            GameOver.SetActive(true);
            SaveToJson();
        }   //GameOver
    }

    public void SaveToJson()
    {
        SaveLoad data = new SaveLoad();
        data.score = score;
        data.numberOfPushedObjects = objectDestroyed;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/BirdgeLearningTech.json", json);
        Debug.Log("Data saved");
    }                   //Json Save Function
}
