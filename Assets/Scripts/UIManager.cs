using UnityEngine;
public class UIManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public EnemySpawnManager EnemySpawnManager;

    public void OnSinglePlayerButtonPressed()
    {
          if (cameraMovement != null)
        {
            cameraMovement.MoveToTarget();
            EnemySpawnManager.StartSpawningEnemies();
        }
        else
        {
            Debug.LogError("CameraMovement script is not assigned in the UIManager.");
        }
    }

}
