
using UnityEngine;

/*
 * - Звук корабля (привязка к нему) (loop)
- Звук выстрел (single)
- Взрыв астероида (single)
- Опасность, малый заряд <20% (loop)
- Подобрали батарейку (Single)
- Взорвались (single)
- Звук нажатия на кнопки
 */
[CreateAssetMenu(menuName = "Game/SoundsConfigure")]
public class SoundsConfigure: ScriptableObject
{
    public AudioClip Ship;
    public AudioClip Shot;
    public AudioClip AsteroidExplosion;
    public AudioClip LowEnergy;
    public AudioClip BatteryPickUp;
    public AudioClip ShipExplosion;
    public AudioClip MenuButtonClick;

    public float LowEnergyWarnCoolDown = 10f;
}
