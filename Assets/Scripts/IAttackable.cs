using UnityEngine;
using System.Collections;

public interface IAttackable {

	void ReceiveHit(int damage, DamageTypes damageType);

	void DestroyMe ();

	int HitPoints ();

//	void RegisterSuccessfulDestroy(float value);

}
