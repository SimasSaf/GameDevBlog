using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public EnemySpawner enemySpawner;

    public void OnSinglePlayerButtonPressed()
    {
          if (cameraMovement != null)
        {
            cameraMovement.MoveToTarget();
            enemySpawner.StartSpawningEnemies();
        }
        else
        {
            Debug.LogError("CameraMovement script is not assigned in the UIManager.");
        }
    }

}
