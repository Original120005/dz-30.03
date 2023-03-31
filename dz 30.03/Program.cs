using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz30_03
{
    public interface IMediator
    {
        void Notify(object sender, string ev);
    }

    class Dispatcher : IMediator
    {
        private Plane plane;
        private Helicopter helicopter;
        private Maiz maiz;

        public Dispatcher(Plane plane, Helicopter helicopter, Maiz maiz)
        {
            this.plane = plane;
            this.plane.SetMediator(this);

            this.helicopter = helicopter;
            this.helicopter.SetMediator(this);

            this.maiz = maiz;
            this.maiz.SetMediator(this);
        }

        public void Notify(object sender, string ev)
        {
            if (ev == "Plane")
            {
                helicopter.FlightWait();
                maiz.FlightWait();
            }

            else if (ev == "Helicopter")
            {
                plane.FlightWait();
                maiz.FlightWait();
            }

            else if (ev == "Maiz")
            {
                plane.FlightWait();
                helicopter.FlightWait();
            }
        }
    }


    class FlyingMachine
    {
        protected IMediator med;

        public FlyingMachine(IMediator med = null)
        {
            this.med = med;
        }
        public void SetMediator(IMediator med)
        {
            this.med = med;
        }
    }

    class Plane : FlyingMachine
    {
        public void FlightStart()
        {
            Console.WriteLine("Plane is starting flyight");
            med.Notify(this, "Plane");
        }
        public void FlightWait()
        {
            Console.WriteLine("Plane is waiting for flyight");
        }
    }

    class Helicopter : FlyingMachine
    {
        public void FlightStart()
        {
            Console.WriteLine("Helicopter is starting flyight");
            med.Notify(this, "Helicopter");
        }
        public void FlightWait()
        {
            Console.WriteLine("Helicopter is waiting for flyight");
        }
    }

    class Maiz : FlyingMachine
    {
        public void FlightStart()
        {
            Console.WriteLine("Maiz is starting flyight");
            med.Notify(this, "Maiz");
        }
        public void FlightWait()
        {
            Console.WriteLine("Maiz is waiting for flyight");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Plane plane = new Plane();
            Helicopter helicopter = new Helicopter();
            Maiz maiz = new Maiz();

            new Dispatcher(plane, helicopter, maiz);

            Console.WriteLine("Client triggers Plane:");
            plane.FlightStart();

            Console.WriteLine();

            Console.WriteLine("Client triggers Maiz:");
            maiz.FlightStart();

        }
    }
}