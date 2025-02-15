using UnityEngine;

public class Battery : ILootableItem
{
    private Sprite _sprite;

    public Battery(Sprite sprite)
    {
        _sprite = sprite;
    }

    public void AddToInventory()
    {
        PlayerManager.Instance.AddItem(this);
    }

    public Sprite GetSprite()
    {
        return _sprite;
    }

    public string GetName()
    {
        return "Battery";
    }

    public void Use()
    {
        PlayerManager.Instance.RechargeFlashlight();
    }
}
