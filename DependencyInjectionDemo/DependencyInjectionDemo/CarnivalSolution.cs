using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemoSolution
{
    class Carnival
    {
        Attendee[] people;


        Carnival()
        {
            people = new Attendee[10];
        }

        class Adult : Attendee
        {
            string job;
            int creditCardNumber;

            public Adult(string job, int creditCardNumber)
            {
                this.job = job;
                this.creditCardNumber = creditCardNumber;
            }

            public void AnnounceCreditCardInformation()
            {
                Console.WriteLine("Hey everyone, this is my credit card number, " + creditCardNumber);
            }
        }


        Attendee[] getPeople()
        {
            return people;
        }

        // BUILIDNG BLOCKS
        abstract class Attendee
        {
            // Stuff all attendees have
            string name;
        }

        class Child : Attendee
        {
            // Your implementation here
            // Stuff a child would do/have
        }

        class Senior : Attendee
        {
            // Your implementation here
            // Stuff a senior would do/have
        }

        // Rename this function to something that makes more sense!
        void AddAttendee(Attendee newAttendee)
        {
            // Your implementation here
            int c = 0;
            while (c < people.Length && people[c] != null)
            {
                c++;
            }

            if (c < people.Length)
            {
                people[c] = newAttendee;
            }

        }
    }
}
