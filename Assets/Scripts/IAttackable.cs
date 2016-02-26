using UnityEngine;
using System.Collections;

public interface IAttackable {

	void ReceiveHit(int damage, DamageTypes damageType);

	string Name();

	void DestroyMe ();

	int HitPoints ();

//	void RegisterSuccessfulDestroy(float value);

}
