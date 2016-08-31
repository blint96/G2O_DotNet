namespace G2O_Framework
{
    using System;
    using System.Drawing;

    public interface ICharacter
    {
        event EventHandler<DeadEventArgs> Died;

        event EventHandler<HealthChangedEventArgs> HealthChanged;

        event EventHandler<HitEventArgs> Hit;

        event EventHandler<MaxHealthChangedEventArgs> MaxHealthChanged;

        event EventHandler<RespawnEventArgs> Respawned;

        event EventHandler<UnconsciousEventArgs> Unconscious;

        event EventHandler<FocusChangedEventArgs> FocusChanged;

        event EventHandler<ItemEquipedEventArgs> ArmorEquiped;

        event EventHandler<ItemEquipedEventArgs> HelmetEquiped;

        event EventHandler<ItemEquipedEventArgs> MeleeWeaponEquiped;

        event EventHandler<ItemEquipedEventArgs> RangedEquiped;

        event EventHandler<ItemEquipedEventArgs> ShieldEquiped;

        event EventHandler<HandItemEquipedEventArgs> HandItemEquiped;


        float Angle { get; set; }

        IClient Client { get; }

        int Dexterity { get; set; }

        ICharacter Focus { get; }

        int Health { get; set; }

        bool IsDead { get; }

        bool IsSpawned { get; }

        bool IsUnconscious { get; }

        int MaxHealth { get; set; }

        string Name { get; set; }

        Color NameColor { get; set; }

        Point3D Position { get; set; }

        int Strength { get; set; }

        int WeaponMode { get; set; }

        int GetAniId();

        int GetSkillWeapon(SkillWeapon skill);

        int GetTalent(Talent talent);

        int PlayAniId(int aniId);

        void SetSkillWeapon(SkillWeapon skill, int value);

        void SetTalent(Talent talent, int value);

        void Spawn();

        void StopAniId();

        void UnspawnPlayer();

        IInventory Inventory { get; }
    }
}