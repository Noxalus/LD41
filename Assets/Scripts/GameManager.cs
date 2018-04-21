using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

  public EnemySpawner enemySpawner;
  public int initialLife;
  public int initialMoney;
  public Text moneyLabel;
  public Text lifeLabel;
  public GameObject gameOverUI;

  private int _life;
  private int _money;
  private bool _gameIsOver;

  void Start()
  {
    _life = initialLife;
    _money = initialMoney;
    _gameIsOver = false;

    UpdateMoneyLabel();
    UpdateLifeLabel();

    gameOverUI.SetActive(false);
  }

  private void Update()
  {
    if (_gameIsOver && Input.GetMouseButtonDown(0))
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  private void UpdateMoneyLabel()
  {
    moneyLabel.text = _money.ToString();
  }

  private void UpdateLifeLabel()
  {
    lifeLabel.text = _life.ToString();
  }

  public bool CanPlaceTower()
  {
    return _money >= 20;
  }

  public void OnBeatMissed()
  {
    if (_gameIsOver)
      return;

    WasteMoney(5);
  }

  public void OnBeatHit()
  {
    // TODO: TowerManager trigger towers' shoot
  }

  private void WasteMoney(int waste)
  {
    // TODO: Play an animation
    _money = Mathf.Max(0, waste);
    UpdateMoneyLabel();
  }

  public void OnEnemyDeath(EnemyAction enemyAction)
  {
    if (_gameIsOver)
      return;

    _money += enemyAction.enemy.moneyDropped;
    UpdateMoneyLabel();
  }

  public void OnEnemyExit(EnemyAction enemyAction)
  {
    if (_gameIsOver)
      return;

    _life--;
    UpdateLifeLabel();

    if (_life == 0)
      GameOver();
  }

  private void GameOver()
  {
    gameOverUI.SetActive(true);
    _gameIsOver = true;
  }
}
