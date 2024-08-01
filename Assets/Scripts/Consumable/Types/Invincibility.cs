using UnityEngine;
using System;
using System.Collections;

public class Invincibility : Consumable
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
        GameObject.Find("CharacterSlot").tag = "temp";
        c.characterCollider.SetInvincibleExplicit(true);
       
       // TrackManager.instance.IsBoostActive = true;
    }

    public override IEnumerator Started(CharacterInputController c)
    {
        yield return base.Started(c);
        c.characterCollider.SetInvincible(duration);
    }

    public override void Ended(CharacterInputController c)
    {
        base.Ended(c);
        // TrackManager.instance.IsBoostActive = false;
        GameObject.Find("CharacterSlot").tag = "Player";
        c.characterCollider.SetInvincibleExplicit(false);
       
    }
}
