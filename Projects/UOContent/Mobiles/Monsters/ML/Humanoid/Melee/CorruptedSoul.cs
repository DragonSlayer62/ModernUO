namespace Server.Mobiles
{
    public class CorruptedSoul : BaseCreature
    {
        [Constructible]
        public CorruptedSoul() : base(AIType.AI_Melee)
        {
            Body = 0x3CA;
            Hue = 0x453;

            SetSpeed(0.25, 5);

            SetStr(102, 115);
            SetDex(101, 115);
            SetInt(203, 215);

            SetHits(61, 69);

            SetDamage(4, 40);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 61, 74);
            SetResistance(ResistanceType.Fire, 22, 48);
            SetResistance(ResistanceType.Cold, 73, 100);
            SetResistance(ResistanceType.Poison, 0);
            SetResistance(ResistanceType.Energy, 51, 60);

            SetSkill(SkillName.MagicResist, 80.2, 89.4);
            SetSkill(SkillName.Tactics, 81.3, 89.9);
            SetSkill(SkillName.Wrestling, 80.1, 88.7);

            Fame = 5000;
            Karma = -5000;

            // VirtualArmor = 6; Not sure
        }

        public CorruptedSoul(Serial serial) : base(serial)
        {
        }

        public override bool DeleteCorpseOnDeath => true;

        public override string DefaultName => "a corrupted soul";

        public override bool AlwaysAttackable => true;
        public override bool BleedImmune => true; // NEED TO VERIFY

        /*public override int GetDeathSound()
        {
          return 0x0;
        }*/

        public override bool AlwaysMurderer => true;

        // NEED TO VERIFY SOUNDS! Known: No Idle Sound.

        /*public override int GetAngerSound()
        {
          return 0x0;
        }*/

        public override int GetAttackSound() => 0x233;

        // TODO: Proper OnDeath Effect

        public override bool OnBeforeDeath()
        {
            if (!base.OnBeforeDeath())
            {
                return false;
            }

            // 1 in 20 chance that a Thread of Fate will appear in the killer's pack

            Effects.SendLocationEffect(Location, Map, 0x376A, 10, 1);
            return true;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }
    }
}
