function onInit()
{
	print("Gamemode init!");
}

addEventHandler("onInit", onInit);

function onPlayerConnect(pid)
{
	print("onPlayerConnect pid: " + pid + " Name: " + getPlayerName(pid));
}

addEventHandler("onPlayerConnect", onPlayerConnect);

function onPlayerJoin(pid)
{
	print("onPlayerJoin pid: " + pid);
	
	setPlayerPosition(pid, 0, 0, 0);
	spawnPlayer(pid);
	
	equipArmor(pid, "ITAR_DIEGO");
	equipMeleeWeapon(pid, "ITMW_1H_VLK_DAGGER");
	equipMeleeWeapon(pid, "ITMW_1H_BLESSED_01");
}

addEventHandler("onPlayerJoin", onPlayerJoin);

function onPlayerRespawn(pid)
{
	print("onPlayerRespawn pid: " + pid);
	
	setPlayerHealth(pid, 4000);
	setPlayerMaxHealth(pid, 4000);
	
	setPlayerPosition(pid, 0, 0, 0);
	
	equipArmor(pid, "ITAR_DIEGO");
	equipMeleeWeapon(pid, "ITMW_1H_VLK_DAGGER");
	equipMeleeWeapon(pid, "ITMW_1H_BLESSED_01");
}

addEventHandler("onPlayerRespawn", onPlayerRespawn);

function onPlayerDead(pid, kid)
{
	print("onPlayerDead pid: " + pid + " kid: " + kid);
	
	currPos = getPlayerPosition(pid);
	currAngle = getPlayerAngle(pid);
}

addEventHandler("onPlayerDead", onPlayerDead);

function onPlayerDisconnect(pid, reason)
{
	print("onPlayerDisconnect pid: " + pid + " reason: " + reason);
}

addEventHandler("onPlayerDisconnect", onPlayerDisconnect);

function onPlayerChangeHealth(pid, oldHp, currHp)
{
	print("onPlayerChangeHealth pid: " + pid + " hp: " + oldHp + "/" + currHp);
}

addEventHandler("onPlayerChangeHealth", onPlayerChangeHealth);

function onPlayerChangeMaxHealth(pid, oldMaxHp, currMaxHp)
{
	print("onPlayerChangeMaxHealth pid: " + pid + " max hp: " + oldMaxHp + "/" + currMaxHp);
}

addEventHandler("onPlayerChangeMaxHealth", onPlayerChangeMaxHealth);

function onPlayerChangeWeaponMode(pid, oldWm, currWm)
{
	print("onPlayerChangeWeaponMode pid: " + pid + " wm: " + oldWm + "/" + currWm);
}

addEventHandler("onPlayerChangeWeaponMode", onPlayerChangeWeaponMode);

function onPlayerChangeFocus(pid, oldFocusId, currFocusId)
{
	print("onPlayerChangeFocus pid: " + pid + " fid: " + oldFocusId + "/" + currFocusId);
}

addEventHandler("onPlayerChangeFocus", onPlayerChangeFocus);

function onPlayerChangeColor(pid, r, g, b)
{
	print("onPlayerChangeColor pid: " + pid + " color: " + r + ", " + g + ", " + b);
}

addEventHandler("onPlayerChangeColor", onPlayerChangeColor);

function onPlayerEquipArmor(pid, instance)
{
	print("onPlayerEquipArmor pid: " + pid + " instance: " + instance);
}

addEventHandler("onPlayerEquipArmor", onPlayerEquipArmor);

function onPlayerEquipHelmet(pid, instance)
{
	print("onPlayerEquipHelmet pid: " + pid + " instance: " + instance);
}

addEventHandler("onPlayerEquipHelmet", onPlayerEquipHelmet);

function onPlayerEquipMeleeWeapon(pid, instance)
{
	print("onPlayerEquipMeleeWeapon pid: " + pid + " instance: " + instance);
}

addEventHandler("onPlayerEquipMeleeWeapon", onPlayerEquipMeleeWeapon);

function onPlayerEquipRangedWeapon(pid, instance)
{
	print("onPlayerEquipRangedWeapon pid: " + pid + " instance: " + instance);
}

addEventHandler("onPlayerEquipRangedWeapon", onPlayerEquipRangedWeapon);

function onPlayerEquipShield(pid, instance)
{
	print("onPlayerEquipShield pid: " + pid + " instance: " + instance);
}

addEventHandler("onPlayerEquipShield", onPlayerEquipShield);

function onPlayerEquipHandItem(pid, hand, instance)
{
	print("onPlayerEquipHandItem pid: " + pid + " hand: " + hand + " instance: " + instance);
}

addEventHandler("onPlayerEquipHandItem", onPlayerEquipHandItem);

function onPlayerChangeBodyState(pid, oldBS, newBS)
{
	print("onPlayerChangeBodyState pid: " + getPlayerName(pid) + " old bs: " + oldBS + " new bs: " + newBS);
}

addEventHandler("onPlayerChangeBodyState", onPlayerChangeBodyState);