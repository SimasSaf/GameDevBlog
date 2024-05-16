using TMPro;
using UnityEngine;
using UnityEngine.UI; // Required for accessing Button components

public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    private GameObject upgradePrefab;
    private IMenuUIController iMenuUIController;
    private ILevelingSystemOR levelingSystemOR;
    private IBulletSpawnManager iBulletSpawnManager;
    string baseName = "Upgrade";
    private int numberOfUpgrades = 4;

    void Awake()
    {
        iMenuUIController = FindObjectOfType<MenuUiController>();
        levelingSystemOR = FindObjectOfType<LevelingSystem>();
        iBulletSpawnManager = FindObjectOfType<BulletSpawnManager>();
    }

    void Start()
    {
        SpawnUpgrades(numberOfUpgrades, new Vector3(-150, 0, 0));
    }

    public void SpawnUpgrades(int count, Vector3 startPosition)
    {
        GameObject upgradeMenu = GameObject.Find("UpgradeMenu");
        if (upgradeMenu == null)
        {
            Debug.LogError("Failed to find UpgradeMenu in the scene.");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(
                startPosition.x + (i * 150),
                startPosition.y,
                startPosition.z
            );

            GameObject newUpgrade = Instantiate(
                upgradePrefab,
                position,
                Quaternion.identity,
                upgradeMenu.transform
            );
            newUpgrade.name = baseName + i;
            newUpgrade.SetActive(true);

            SetupButton(newUpgrade, i);
            SetupUpgradeName(newUpgrade, i);
            newUpgrade.transform.localPosition = position;
        }
    }

    void SetupButton(GameObject upgrade, int index)
    {
        Button button = upgrade.transform.GetChild(1).GetComponent<Button>();
        if (button != null)
        {
            button.onClick.RemoveAllListeners();

            switch (index)
            {
                case 0:
                    button.onClick.AddListener(() =>
                    {
                        // damage
                        iBulletSpawnManager.IncreaseBulletProperties(10, null, null, null);
                        iMenuUIController.ResumeGame();
                    });
                    break;
                case 1:
                    button.onClick.AddListener(() =>
                    {
                        // fire rate
                        iBulletSpawnManager.IncreaseBulletProperties(null, 0.5f, null, null);
                        iMenuUIController.ResumeGame();
                    });
                    break;
                case 2:
                    button.onClick.AddListener(() =>
                    {
                        // split level
                        iBulletSpawnManager.IncreaseBulletProperties(null, null, 1, null);
                        iMenuUIController.ResumeGame();
                    });
                    break;
                case 3:
                    button.onClick.AddListener(() =>
                    {
                        // fire level
                        iBulletSpawnManager.IncreaseBulletProperties(null, null, null, 1);
                        iMenuUIController.ResumeGame();
                    });
                    break;
            }
        }
        else
        {
            Debug.LogError("Button component not found on " + upgrade.name);
        }
    }

    void SetupUpgradeName(GameObject upgrade, int index)
    {
        TextMeshProUGUI upgradeName = upgrade.GetComponentInChildren<TextMeshProUGUI>();
        if (upgradeName != null)
        {
            string[] names = { "Damage", "Fire Rate", "Split Level", "Fire Level" };
            upgradeName.text = names[index];
        }
        else
        {
            Debug.LogError("Failed to find TextMeshProUGUI on " + upgrade.name);
        }
    }
}
