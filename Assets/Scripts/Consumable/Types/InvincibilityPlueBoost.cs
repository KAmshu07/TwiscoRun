using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPlueBoost : Consumable
{
    public override string GetConsumableName()
    {
        return "Invincible";
    }

    public override ConsumableType GetConsumableType()
    {
        return ConsumableType.INVINCIBILITY;
    }

    public override int GetPrice()
    {
        return 1500;
    }

    public override int GetPremiumCost()
    {
        return 5;
    }

    public override void Tick(CharacterInputController c)
    {
        base.Tick(c);
        c.characterCollider.SetInvincibleExplicit(true);
        GameObject.Find("CharacterSlot").tag = "temp";
        TrackManager.instance.IsBoostActive = true;
    }

    public override IEnumerator Started(CharacterInputController c)
    {
        yield return base.Started(c);
       c.characterCollider.SetInvincible(duration);
    }

    public override void Ended(CharacterInputController c)
    {
        base.Ended(c);
        TrackManager.instance.IsBoostActive = false;
        c.characterCollider.SetInvincibleExplicit(false);
         GameObject.Find("CharacterSlot").tag = "Player";

    }
}
