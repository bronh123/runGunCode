using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WaveDisplay : MonoBehaviour
{

    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private TextMeshProUGUI waveDisplayText;
    private int currentWave;

    void Start()
    {
        if (enemyManager == null)
        {
            enemyManager = FindFirstObjectByType<EnemyManager>();
            currentWave = enemyManager.GetWaveNumber();
        }

    }
    void Update()
    {
        if (currentWave < enemyManager.GetWaveNumber() && waveDisplayText != null)
        {
            currentWave = enemyManager.GetWaveNumber();
            waveDisplayText.text = $"Wave {currentWave}";
        }
    }
}
