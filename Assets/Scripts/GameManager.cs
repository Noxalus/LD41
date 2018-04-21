using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public EnemySpawner enemySpawner;
    public int initialLife;
    public int initialMoney;
    public Text moneyLabel;
    public Text lifeLabel;
    public GameObject gameOverUI;

    private int _life;
    private int _money;

	void Start ()
    {
        _life = initialLife;
        _money = initialMoney;

        UpdateMoneyLabel();
        UpdateLifeLabel();
    }

    private void UpdateMoneyLabel()
    {
        moneyLabel.text = _money.ToString();
    }

    private void UpdateLifeLabel()
    {
        lifeLabel.text = _life.ToString();
    }

    public void OnEnemyDeath(EnemyAction enemyAction)
    {
        _money += enemyAction.enemy.moneyDropped;
        UpdateMoneyLabel();
    }

    public void OnEnemyExit(EnemyAction enemyAction)
    {
        _life--;
        UpdateLifeLabel();

        if (_life == 0)
            GameOver();
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
    }
}
