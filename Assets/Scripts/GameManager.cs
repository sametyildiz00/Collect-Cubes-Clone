using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerMovement player = default;
    [SerializeField] List<GameObject> levelList = default;
    [SerializeField] Image mask = default;
    [SerializeField] Image blackScreen = default;
    [SerializeField] Text currentLevel = default;
    [SerializeField] Text nextLevel = default;
    [SerializeField] Text blackScreenText = default;
    [SerializeField] Image inputSystem = default;
    [SerializeField] Button restartButton = default;
    public int levelCubeCount;
    public int currentCubeCount;
    GameObject selectedLevel;
    GameObject spawnedLevel;
    Vector3 playerStartPos;
    float fillAmount = 0;
    int levelIndex = 0;

    private void Awake()
    {
        playerStartPos = new Vector3(0, 0, -8);
        LevelSpawn();
    }
    public void ProggressBar()
    {
        fillAmount = (float)currentCubeCount / (float)levelCubeCount;
        mask.fillAmount = fillAmount;
    }

    void NextLevel()
    {
        Destroy(spawnedLevel);
        if (levelIndex < levelList.Count - 1)
        {
            levelIndex++;
        }
        LevelSpawn();
    }
    public void LevelCompleted()
    {
        if (currentCubeCount == levelCubeCount)
        {
            inputSystem.gameObject.SetActive(false);
            player.rb.velocity = Vector3.zero;
            restartButton.gameObject.SetActive(false);
            LeanTween.alpha(blackScreen.rectTransform, 1f, 1f).setOnComplete(() =>
                {
                    NextLevel();
                    LeanTween.alpha(blackScreen.rectTransform, 0f, 1f);
                    restartButton.gameObject.SetActive(true);
                });
        }
    }
    public void RestartLevel()
    {
        Destroy(spawnedLevel);
        LevelSpawn();
    }
    public void LevelSpawn()
    {
        selectedLevel = levelList[levelIndex];
        spawnedLevel = Instantiate(selectedLevel, selectedLevel.transform.position, Quaternion.identity);
        levelCubeCount = spawnedLevel.transform.childCount;
        currentCubeCount = 0;
        currentLevel.text = (levelIndex + 1).ToString();
        nextLevel.text = (levelIndex + 2).ToString();
        mask.fillAmount = 0;
        player.transform.position = playerStartPos;
        player.rb.velocity = Vector3.zero;
        player.transform.rotation = default;
        player.gameObject.SetActive(true);
        inputSystem.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(spawnedLevel);
        LevelSpawn();
    }
}
