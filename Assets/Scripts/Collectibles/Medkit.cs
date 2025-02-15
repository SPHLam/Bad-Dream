using UnityEngine;

public class Medkit : ILootableItem
{
    private Sprite _sprite;

    public Medkit(Sprite sprite)
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
        return "Medkit";
    }

    public void Use()
    {
        PlayerManager.Instance.Heal();
    }
}
