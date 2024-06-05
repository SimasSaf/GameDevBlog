using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    private GameObject upgradePrefab;
    private IMenuUIController iMenuUIController;
    private ILevelingSystemOR levelingSystemOR;
    private IBulletSpawnManager iBulletSpawnManager;
    private string baseName = "Upgrade";
    private int numberOfUpgrades = 4;
    private GameObject[] upgradeInstances;

    void Awake()
    {
        iMenuUIController = FindObjectOfType<MenuUiController>();
        levelingSystemOR = FindObjectOfType<LevelingSystem>();
        iBulletSpawnManager = FindObjectOfType<BulletSpawnManager>();

        // Initialize upgrades
        InitializeUpgrades(numberOfUpgrades, new Vector3(-220, 0, 0));
    }

    void Start()
    {
        // Make upgrades visible
        MakeUpgradesVisible();
    }

    private void InitializeUpgrades(int count, Vector3 startPosition)
    {
        GameObject upgradeMenu = GameObject.Find("UpgradeMenu");
        if (upgradeMenu == null)
        {
            Debug.LogError("Failed to find UpgradeMenu in the scene.");
            return;
        }

        upgradeInstances = new GameObject[count];

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
            newUpgrade.SetActive(false);

            SetupButton(newUpgrade, i);
            SetupUpgradeName(newUpgrade, i);
            newUpgrade.transform.localPosition = position;

            upgradeInstances[i] = newUpgrade;
        }
    }

    private void MakeUpgradesVisible()
    {
        foreach (var upgrade in upgradeInstances)
        {
            upgrade.SetActive(true);
        }
    }

    private void SetupButton(GameObject upgrade, int index)
    {
        Button button = upgrade.transform.GetChild(1).GetComponent<Button>();
        if (button != null)
        {
            // Copy the Button settings from the prefab to ensure colors are transferred
            Button prefabButton = upgradePrefab.transform.GetChild(1).GetComponent<Button>();
            if (prefabButton != null)
            {
                button.colors = prefabButton.colors;
                button.transition = prefabButton.transition;
                button.spriteState = prefabButton.spriteState;
            }

            button.onClick.RemoveAllListeners();

            switch (index)
            {
                case 0:
                    button.onClick.AddListener(() =>
                    {
                        // damage
                        iBulletSpawnManager.IncreaseBulletProperties(5, null, null, null);
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

    private void SetupUpgradeName(GameObject upgrade, int index)
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
