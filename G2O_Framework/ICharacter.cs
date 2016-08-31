using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
    using System.Drawing;

    public interface ICharacter
    {
        IClient Client { get; }

        void Spawn();

        void UnspawnPlayer();

        bool IsSpawned { get; }

        bool IsDead { get; }

        bool IsUnconscious { get; }

        ICharacter Focus { get; }

        string Name { get; set; }

        Color NameColor { get; set; }

        Point3D Position { get; set; }

        float Angle { get; set; }

        int Health { get; set; }

        int MaxHealth { get; set; }

        int WeaponMode { get; set; }

        int Strength { get; set; }

        int Dexterity { get; set; }

        void SetSkillWeapon(int skill, int value);

        int GetSkillWeapon(int skill);

        void SetTalent(int talent, int value);

        int GetTalent(int talent);

        int GetAniId();

        int PlayAniId(int aniId);

        void StopAniId();

        event EventHandler<HealthChangedEventArgs> HealthChanged;

        event EventHandler<MaxHealthChangedEventArgs> MaxHealthChanged;

    }
}
