using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour, IIngameUI
{
    private TextMeshProUGUI healthCounter;
    private TextMeshProUGUI level;
    private Image expBarFill;

    void Awake()
    {
        GameObject healthCounterObject = GameObject.Find("HealthCounter");
        GameObject levelObject = GameObject.Find("Level");
        GameObject expBarFillObject = GameObject.Find("expBarFill");


        level = levelObject.GetComponent<TextMeshProUGUI>();
        healthCounter = healthCounterObject.GetComponent<TextMeshProUGUI>();

        expBarFill = expBarFillObject.GetComponent<Image>();
    }

    void Start()
    {
        ResetLevelAndExperience();
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthCounter.text = "Health: " + currentHealth + "/" + maxHealth;
    }

    public void ResetLevelAndExperience()
    {
        expBarFill.fillAmount = 0.0f;
        level.text = "0";
    }

    public void LevelUp(int level)
    {
        expBarFill.fillAmount = 0.0f;
        this.level.text = "" + level;
    }

    public void AddExperience(int experience, int experienceToNextLevel)
    {
        expBarFill.fillAmount = (float)experience / experienceToNextLevel;
    }
}
