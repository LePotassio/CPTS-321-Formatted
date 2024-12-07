using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationDemoSolution
{
    interface Tool
    {
        // Something all tools do
        void Use();
    }

    interface ChargedElectronic
    {
        void ChargeBattery();
    }

    interface ScrewManipulator
    {
        void Screw();
        void Unscrew();
    }

    interface Explosive
    {
        void Detonate();
    }

    interface WritingUtensil
    {
        void Draw();
    }

    interface CookingUtensil
    {
        void Cook();
    }

    interface HasCleanliness
    {
        void CleanTool();
        double GetDirtiness();
    }

    interface Timer
    {
        void Countdown();
    }

    interface Heater
    {
        void Heat();
    }

    class Dynamite : Tool, Explosive, Timer
    {
        public void Countdown()
        {
            Console.WriteLine("3...2...1...");
        }

        public void Detonate()
        {
            Console.WriteLine("BOOM (goes the dynamite)");
        }

        public void Use()
        {
            Countdown();
            Detonate();
        }
    }

    class Oven : Tool, CookingUtensil, Heater, HasCleanliness
    {
        internal double dirtiness;

        public Oven()
        {
            dirtiness = 0;
        }

        public void CleanTool()
        {
            dirtiness = 5;
        }

        public void Cook()
        {
            Console.WriteLine("Oven is cooking!");
        }

        public double GetDirtiness()
        {
            return dirtiness;
        }

        public void Heat()
        {
            Console.WriteLine("Oven is heating up!");
        }

        public void Use()
        {
            Heat();
            Cook();
            dirtiness += 5;
        }
    }

    class BlastFurnace : Tool, Explosive, Heater, HasCleanliness
    {
        double dirtiness;

        public BlastFurnace()
        {
            dirtiness = 0;
        }

        public void CleanTool()
        {
            dirtiness = 15;
        }

        public void Detonate()
        {
            Console.WriteLine("Furnace is combusting!");
            Heat();
        }

        public double GetDirtiness()
        {
            return dirtiness;
        }

        public void Heat()
        {
            Console.WriteLine("Furnace is heating the material!");
        }

        public void Use()
        {
            Detonate();
            dirtiness += 25;
        }
    }

    class PowerDrill : Tool, ChargedElectronic, ScrewManipulator, HasCleanliness
    {
        double dirtiness;
        double ampCharge;

        public PowerDrill()
        {
            dirtiness = 0;
            ampCharge = 90;
        }

        public void ChargeBattery()
        {
            ampCharge = 100;
        }

        public void CleanTool()
        {
            dirtiness = 5;
        }

        public double GetDirtiness()
        {
            return dirtiness;
        }

        public void Screw()
        {
            Console.WriteLine("Screw is screwed");
        }

        public void Unscrew()
        {
            Console.WriteLine("Screw is unscrewed");
        }

        public void Use()
        {
            if (ampCharge >= 10)
            {
                // Not really how drills work...
                Screw();
                Unscrew();
                ampCharge -= 10;
                dirtiness += 5;
            }
        }
    }

    class Microwave : Oven, Timer
    {
        public Microwave()
        {
            dirtiness = 0;
        }

        public new void CleanTool()
        {
            dirtiness = 0;
        }

        public new void Cook()
        {
            Console.WriteLine("Microwave is cooking!");
        }

        public void Countdown()
        {
            Console.WriteLine("5...4...3...2...1...");
        }

        public new void Heat()
        {
            Console.WriteLine("Microwave is heating up!");
        }
    }
}
