using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationDemo
{
    interface Tool
    {
        void Heat();
        void Screw();
        void Unscrew();
        void ChargeBattery();
        void Countdown();
        void Detonate();
        void Draw();
        void Cook();

        void CleanTool();
        double GetDirtiness();

        // Something all tools do
        void Use();
    }

    class Dynamite : Tool
    {
        public void ChargeBattery()
        {
            throw new NotImplementedException();
        }

        public void CleanTool()
        {
            throw new NotImplementedException();
        }

        public void Cook()
        {
            throw new NotImplementedException();
        }

        public void Countdown()
        {
            throw new NotImplementedException();
        }

        public void Detonate()
        {
            Console.WriteLine("BOOM (goes the dynamite)");
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public double GetDirtiness()
        {
            throw new NotImplementedException();
        }

        public void Heat()
        {
            throw new NotImplementedException();
        }

        public void Screw()
        {
            throw new NotImplementedException();
        }

        public void Unscrew()
        {
            throw new NotImplementedException();
        }

        public void Use()
        {
            Detonate();
        }
    }

    class Oven : Tool
    {
        double dirtiness;

        public Oven()
        {
            dirtiness = 0;
        }

        public void ChargeBattery()
        {
            throw new NotImplementedException();
        }

        public void CleanTool()
        {
            dirtiness = 0;
        }

        public void Cook()
        {
            Console.WriteLine("Oven is cooking!");
        }

        public void Countdown()
        {
            throw new NotImplementedException();
        }

        public void Detonate()
        {
            throw new NotImplementedException();
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public double GetDirtiness()
        {
            return dirtiness;
        }

        public void Heat()
        {
            Console.WriteLine("Oven is heating up!");
        }

        public void Screw()
        {
            throw new NotImplementedException();
        }

        public void Unscrew()
        {
            throw new NotImplementedException();
        }

        public void Use()
        {
            Heat();
            Cook();
            dirtiness += 5;
        }
    }

    class BlastFurnace : Tool
    {
        double dirtiness;

        public BlastFurnace()
        {
            dirtiness = 0;
        }

        public void ChargeBattery()
        {
            throw new NotImplementedException();
        }

        public void CleanTool()
        {
            dirtiness = 0;
        }

        public void Cook()
        {
            throw new NotImplementedException();
        }

        public void Countdown()
        {
            throw new NotImplementedException();
        }

        public void Detonate()
        {
            Console.WriteLine("Furnace is combusting!");
            Heat();
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public double GetDirtiness()
        {
            return dirtiness;
        }

        public void Heat()
        {
            Console.WriteLine("Furnace is heating the material!");
        }

        public void Screw()
        {
            throw new NotImplementedException();
        }

        public void Unscrew()
        {
            throw new NotImplementedException();
        }

        public void Use()
        {
            Detonate();
            dirtiness += 25;
        }
    }
}
