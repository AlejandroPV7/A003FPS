using TMPro;
using UnityEngine;

// Se encarga de actualizar el HUD (nombre del arma y municion)
public class UIManager : MonoBehaviour
{
    public WeaponManager weaponManager;
    public TextMeshProUGUI textoArma;
    public TextMeshProUGUI textoMunicion;

    private Weapon armaActual;

    void Start()
    {
        if (weaponManager == null)
            weaponManager = FindFirstObjectByType<WeaponManager>();

        // cuando cambiamos de arma se llama a CambiarArma
        weaponManager.ActiveWeaponChanged += CambiarArma;
        CambiarArma(weaponManager.ActiveWeapon);
    }

    void OnDestroy()
    {
        if (weaponManager != null)
            weaponManager.ActiveWeaponChanged -= CambiarArma;

        if (armaActual != null)
            armaActual.AmmoChanged -= ActualizarMunicion;
    }

    void CambiarArma(Weapon arma)
    {
        // dejamos de escuchar el arma anterior
        if (armaActual != null)
            armaActual.AmmoChanged -= ActualizarMunicion;

        armaActual = arma;

        // si no llevamos arma no se muestra nada
        if (armaActual == null)
        {
            textoArma.text = "";
            textoMunicion.text = "";
            return;
        }

        armaActual.AmmoChanged += ActualizarMunicion;
        textoArma.text = armaActual.Data.displayName;
        ActualizarMunicion(armaActual);
    }

    void ActualizarMunicion(Weapon arma)
    {
        textoMunicion.text = arma.Magazine + " / " + arma.Data.magazineSize;
    }
}
