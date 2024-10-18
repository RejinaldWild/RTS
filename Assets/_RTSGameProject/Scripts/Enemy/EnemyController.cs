using System.Collections.Generic;

public class EnemyController
{
    private List<Enemy> _enemys;

    public EnemyController(List<Enemy> enemys)
    {
        _enemys = enemys;
    }

    public void FixedUpdate()
    {
        foreach (var enemyUnit in _enemys)
        {
            enemyUnit.Move();
        }
    }
}
