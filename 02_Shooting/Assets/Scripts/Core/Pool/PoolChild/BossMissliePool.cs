using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissliePool : ObjectPool<BossMisslie>
{
    // BossMisslie는 Enemy지만 EnemyObjectPool를 쓰지 않는 이유는,
    // 터트려도 점수를 안주기 때문에 궂이 추가할 필요가 없음
}
