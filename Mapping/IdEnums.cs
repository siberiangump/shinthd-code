public enum CharacterId
{
    Test = -1,
    Nome = 0,
    // hero
    Rogue = 10,
	Cleric = 11, 
	Warrior = 12, 
	Mage = 13,
    // monsters
    Risen = 20, 
	Lich = 21,
	Rat = 22
}

// 0XXX character 
public enum SkillId
{
	None = -1,
	// common 0000
	Move = 0000,
	Idle = 0001,
	Dodge = 0002,
	GetDamage = 0003,
	Dead = 0004,
	Charge = 0005,
	IdleCharge = 0006,

	// Rogue  1000
	PistolShoot = 1001, // {ranged, 1 target, 1D6}
	Mockery = 1002, // {any, 1 target, CD 2}
	// Cleric 1100
	VampireClaws = 1101, // {Melee, 1 target, 1D4}
	VampireHealing = 1102, // {any, 1 target, CD 3, -1 cd on any }
	// Warrior 1200
	HammerStrike = 1201, // {Melee, 1 target, 1x6}
	HammerSwing = 1202, // {Arc , all in pattern, 1D6}
	// Mage 1300
	FlameBreath = 1301, // {InFront, all in pattern, 1D4}
	LavaFloor = 1302, // {InFront, all in pattern, 1D4}
	// Zombi 2000
	ZombiStrike = 2001,

	// Lich 2100
	RizeDead = 2101, // {Charge 2, place up to 3 zombi on free spots (first line in priority)} 
	BuffDead = 2102, // {CD 2,  all zombi in field up attack do next D add +2 health and full heal}
	AttackMurena = 2103, // { create 2 that will melee attack random line }
	AttackMurenaEnhance = 2104, // { create 2 that will melee attack random line }
	ConsumeDead = 2105, // { passive, heal 1d4 for each dead frendly creature }
	Resurect = 2106, // { passive, when going dead add +2 health and full heal and spawn 3 zombi}


	// Rat 2200
	RatBite = 2201, // {Melee, 1 target, 1D4}
	RatSwing = 2202 // {Move and after + Melee, 1 target, 1D4}

}

public enum Patterns 
{
	None = -1,
	MeeleForward = 0,
	MeeleArc = 1,
	Ranged = 2,
	
}