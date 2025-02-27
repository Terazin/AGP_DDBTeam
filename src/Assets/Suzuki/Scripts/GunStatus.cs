using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunStatus : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;

    int remainBullets;
    [SerializeField] float firstShotIntarval = 2f, defaultShotIntarval = 0.5f;

    public float FirstIntarval => firstShotIntarval;
    public float DefaultIntarval => defaultShotIntarval;

    public int RemainBullets
    {
        get { return remainBullets; }
    }

    public Sprite WeaponImage
    {
        get { return weaponData.WeaponImage; }
    }

    public bool IsSubWeapon
    {
        get { return weaponData.SubWeapon == null; }
    }
    public WeaponData.WeaponType WeaponType
    {
        get { return weaponData.Type; }
    }

    void Start()
    {
        FillBullet();
    }
    /// <summary>
    /// 銃から発射方向を向いた銃弾を生成する
    /// </summary>
    /// <param name="infiniteBullet">弾を消費させるかどうか</param>
    /// <returns>撃てたかどうかを返す</returns>
    public bool Shoot(Vector3 position, Vector3 forward, string tag, bool infiniteBullet)
    {
        if(remainBullets <= 0 && !infiniteBullet) return false;
        if(!infiniteBullet) remainBullets--;

        for (int i = 0; i < weaponData.BulletSettings.Count; i++)
        {
            GameObject bullet = Instantiate(weaponData.BulletPrefab, position, Quaternion.identity);

            Vector3 diffusion = new Vector3(weaponData.BulletSettings[i].Diffusion.x * Random.Range(1 - weaponData.BulletSettings[i].RandomNess, 1),
                weaponData.BulletSettings[i].Diffusion.y * Random.Range(1 - weaponData.BulletSettings[i].RandomNess, 1), 0f);

            bullet.tag = tag == "Player" ? "PlayerBullet" : "EnemyBullet";
            bullet.transform.forward = forward;
            bullet.transform.Rotate(diffusion);
        }
        SR_SoundController.instance.PlaySEOnce(weaponData.ShotSound, transform);// 銃声を鳴らす
        if (remainBullets == 0 && weaponData.Role == WeaponData.WeaponRole.Main)
        {
            ChangeWeapon();
        }
        return true;
    }

    void FillBullet()// 弾丸の補充
    {
        remainBullets = weaponData.MaxBullet;
    }

    void ChangeWeapon()// 武器の切り替え
    {
        if (weaponData.SubWeapon != null)
        {
            Debug.Log("サブ武器を使う！");
            Transform parent = this.transform.parent;
            Instantiate(weaponData.SubWeapon, parent);// 武器の生成
            PlayerMove playerMove = parent.GetComponentInChildren<PlayerMove>();

            this.transform.SetParent(null);// 親子付けを外す
            playerMove.SetGunObject();// 持っている銃の設定
            Destroy(this.gameObject);
        }
    }
}
