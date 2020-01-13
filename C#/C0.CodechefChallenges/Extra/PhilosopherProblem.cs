using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace C0.CodechefChallenges.Extra
{
    abstract class Philosopher
    {
        protected static Random Random = new Random();
        protected Timer Timer = new Timer();

        public string Name { get; protected set; }
        public Plate Plate { get; protected set; }

        public abstract void Think();
        public abstract void Eat();
        
        public bool IsEating { get; protected set; }

        public void Start() => this.Timer.Start();
        public void Stop() => this.Timer.Stop();

        protected Philosopher(Plate plate, string name)
        {
            this.Plate = plate;
            this.Name = name;
            Timer.Elapsed += Timer_Elapsed;
            Timer.Interval = 2000;
            Timer.AutoReset = true;
        }

        protected void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Timer.Stop();

            if (this.IsEating)
            {
                this.Think();
            }
            else
            {
                this.Eat();
            }
        }
    }

    class Fork
    {
        public enum ForkPosition
        {
            LEFT = 1,
            RIGHT = 2
        }

        public readonly object Lock = new object();
        public readonly Guid ID;
        public readonly ForkPosition Position;
        public readonly Plate Plate;

        public Philosopher Owner;
        

        public Fork(Plate plate, ForkPosition pos)
        {
            this.Plate = plate;
            this.Position = pos;
            this.ID = Guid.NewGuid();
        }

        public void GetOwned(Philosopher philosopher)
        {
            if (Owner != null)
            {
                Console.WriteLine($"!!!!! GetOwned: The Fork on the {Position} side of Plate {Plate.ID} is already owned by Philosopher {Owner.Name}!");
                philosopher.Stop();
            }
            else
            {
                Owner = philosopher;
                Console.WriteLine($"    {this.Position} Fork of Plate {this.Plate.ID} was picked up by {philosopher.Name}");
            }
        }

        public void GetReleased(Philosopher philosopher)
        {
            if (Owner != philosopher)
            {
                Console.WriteLine($"!!!!! GetReleased: The Fork on the {Position} side of Plate {Plate.ID} is not owned by Philosopher {philosopher.Name}!");
                philosopher.Stop();
            }
            else
            {
                Owner = null;
                Console.WriteLine($"    {this.Position} Fork of Plate {this.Plate.ID} was dropped by {philosopher.Name}");
            }
        }

    }

    class Table
    {
        public readonly Plate[] Plates;

        public Table()
        {
            this.Plates = new Plate[4] { new Plate("Unten"), new Plate("Rechts"), new Plate("Oben"), new Plate("Links") };

            Fork ful1 = new Fork(Plates[0], Fork.ForkPosition.LEFT);
            Fork ful2 = new Fork(Plates[0], Fork.ForkPosition.RIGHT);

            Fork fr1 = new Fork(Plates[1], Fork.ForkPosition.LEFT);
            Fork fr2 = new Fork(Plates[1], Fork.ForkPosition.RIGHT);

            Fork fo1 = new Fork(Plates[2], Fork.ForkPosition.LEFT);
            Fork fo2 = new Fork(Plates[2], Fork.ForkPosition.RIGHT);

            Plates[3].LeftFork = ful1;
            Plates[3].RightFork = ful2;

        }
    }

    class Plate
    {
        public readonly object Lock = new object();
        public readonly Guid ID;
        public Fork LeftFork { get;  set; }
        public Fork RightFork { get;  set; }
        public readonly string Name;
        public Philosopher SittingPhilosopher { get; private set; }

        public Plate(string name)
        {
            this.ID = Guid.NewGuid();
            this.Name = name;
        }

        public void PlaceForks(Fork left, Fork right)
        {
            this.LeftFork = left;
            this.RightFork = right;
        }
    }










    class PhilosopherRight : Philosopher
    {

        private void Wait(int seconds)
        {
            Console.WriteLine($"{this.Name} waits for {seconds} seconds.");
            this.Timer.Interval = seconds * 1000;
            this.Timer.Start();
            Console.WriteLine("....");
        }
        public override void Eat()
        {
                Console.WriteLine($"{this.Name} beginns eating.");
                Console.WriteLine($"{this.Name} picks Fork {this.Plate.LeftFork.Position} up.");
                this.Plate.LeftFork.GetOwned(this);
                Console.WriteLine($"{this.Name} picks Fork {this.Plate.RightFork.Position} up.");
                this.Plate.RightFork.GetOwned(this);
                this.IsEating = true;

            this.Wait(Random.Next(2,10));
        }

        public override void Think()
        {
                Console.WriteLine($"{this.Name} beginns thinking.");
                Console.WriteLine($"{this.Name} dropps Fork {this.Plate.LeftFork.Position}.");
                this.Plate.LeftFork.GetReleased(this);
                Console.WriteLine($"{this.Name} dropps Fork {this.Plate.RightFork.Position}.");
                this.Plate.RightFork.GetReleased(this);
                this.IsEating = false;


            this.Wait(Random.Next(5, 8));
        }

        public PhilosopherRight(Plate plate, string name) : base(plate, name)
        {
        }
    }














    class PhilosopherWrong : Philosopher
    {
        
        public override void Eat()
        {
            Console.WriteLine($"{this.Name} eats.");
            this.Plate.LeftFork.GetOwned(this);
            this.Plate.RightFork.GetOwned(this);
            this.IsEating = true;
        }

        public override void Think()
        {
            Console.WriteLine($"{this.Name} thinks.");
            this.Plate.LeftFork.GetReleased(this);
            this.Plate.RightFork.GetReleased(this);
            this.IsEating = false;
        }

        public PhilosopherWrong(Plate plate, string name) : base(plate, name)
        {

        }
    }

}
