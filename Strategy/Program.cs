using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player();
            IMonster monster = new EliteZombie();
            monster.SetBehavior(new NormalUnawareMonsterBehavior());

            while (true)
            {
                monster.Update(new GameTime());
                if (monster.CanSee(player))
                {
                    if (monster.Health / monster.MaxHealth < 0.4)
                    {
                        monster.SetBehavior(new EnragedMonsterBehavior());
                    }
                    else
                    {
                        monster.SetBehavior(new AwareButStealthMonsterBehavior());
                    }
                }
            }
        }
    }

    internal class EnragedMonsterBehavior : IMonsterBehavior
    {
        public bool ShouldAttack(Point position, int attackRange) => true; // shoots lazers

        public void UpdateVelocity(int[] velocity)
        {
            if (velocity[0] < 100) velocity[0] += 4;
        }
    }

    internal class AwareButStealthMonsterBehavior : IMonsterBehavior
    {
        public bool ShouldAttack(Point position, int attackRange) => position.X <= attackRange;

        public void UpdateVelocity(int[] velocity) => velocity[0] += 4;
    }

    internal class NormalUnawareMonsterBehavior : IMonsterBehavior
    {
        public bool ShouldAttack(Point position, int attackRange) => false;

        public void UpdateVelocity(int[] velocity) => velocity[0] += 1;
    }

    public struct GameTime { }
    public struct Player { }

    public class EliteZombie : IMonster
    {
        private int attackRange = 15;
        private Point position = new Point(0, 0);
        private int[] velocity = new int[] { 0, 0 };

        private IMonsterBehavior behavior;

        public int MaxHealth => 300;

        public int Health { get; set; } = 300;

        public bool CanSee(Player player) => true;

        public void SetBehavior(IMonsterBehavior behavior)
        {
            this.behavior = behavior ?? throw new ArgumentNullException("Invalid behavior", nameof(behavior));
        }

        public void Update(GameTime gameTime)
        {
            behavior?.UpdateVelocity(velocity);
            if (behavior != null && behavior.ShouldAttack(position, attackRange))
            {
                // attack!
            }
        }
    }

    public interface IMonsterBehavior
    {
        bool ShouldAttack(Point position, int attackRange);
        void UpdateVelocity(int[] velocity);
    }

    public interface IMonster
    {
        int Health { get; set; }
        int MaxHealth { get; }

        void Update(GameTime gameTime);

        void SetBehavior(IMonsterBehavior behavior);
        bool CanSee(Player player);
    }
}
    