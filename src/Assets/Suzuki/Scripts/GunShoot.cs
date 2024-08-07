using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    /// <summary>
    /// 銃から発射方向を向いた銃弾を生成する
    /// </summary>
    /// <param name="infiniteBullet">弾を消費させるかどうか</param>
    /// <returns>撃てたかどうかを返す</returns>
    public bool Shoot(GameObject bulletPrefab, Vector3 position, Vector3 direction, string tag, int remainBullets, bool infiniteBullet)
    {
        if(remainBullets <= 0 && !infiniteBullet) return false;
        if(!infiniteBullet) remainBullets--;
        
        GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        bullet.tag = tag;
        bullet.transform.LookAt(direction);
        return true;
    }
}
