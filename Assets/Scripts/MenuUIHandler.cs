using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public static MenuUIHandler Instance;
    public TextMeshProUGUI HighScoreText;
    public TMP_InputField NameInput;
    public string PlayerName;
    public int HighScore;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        LoadHighScorePlayer();
        HighScoreText.text = "Best Score: " + MenuUIHandler.Instance.PlayerName + " : " + MenuUIHandler.Instance.HighScore;
    }
   
    public void StartNew()
    {
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    [System.Serializable]
    class SaveData
    {
        public string PlayerName;
        public int HighScore;
    }

    public void SaveHighScorePlayer()
    {
        SaveData data = new SaveData();
        data.PlayerName = PlayerName;
        data.HighScore = HighScore;
        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScorePlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            PlayerName = data.PlayerName;
            HighScore = data.HighScore;
        }
    }
}
