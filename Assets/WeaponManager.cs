using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WeaponManager))]
public class WeaponManager : NetworkBehaviour {

    [SerializeField]
    private string weaponLayerName = "Weapon";

    [SerializeField]
    private Transform weaponHolder;

    [SerializeField]
    private PlayerWeapon primaryWeapon;

    private PlayerWeapon currentWeapon;
    private WeaponGraphics currenGraphics;

    private void Start()
    {
        EquipWeapon(primaryWeapon);
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
    public WeaponGraphics GetCurrentGraphics()
    {
        return currenGraphics;
    }
    void EquipWeapon(PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;

        GameObject _weaponIns = Instantiate(_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        _weaponIns.transform.SetParent(weaponHolder);

        currenGraphics = _weaponIns.GetComponent<WeaponGraphics>();
        if(currenGraphics == null)
        {
            Debug.LogError("No WeaponGraphics component on the weapon object" + _weaponIns.name);
        }

        if (isLocalPlayer)
        {
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));
        }
    }
}
