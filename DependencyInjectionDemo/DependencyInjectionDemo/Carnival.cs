using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo
{
    class Carnival
    {
        Adult[] people;


        class Adult {
            string name;
            string job;
            int creditCardNumber;
            int cvcNumber;

            public Adult(string job, int creditCardNumber, int cvcNumber)
            {
                this.job = job;
                this.creditCardNumber = creditCardNumber;
                this.cvcNumber = cvcNumber;
            }

            public void AnnounceCreditCardInformation()
            {
                Console.WriteLine("Hey everyone, this is my credit card number, " + creditCardNumber + ". The 3 digit code on the back is " + cvcNumber + ".");
            }
        }


        Adult[] getPeople()
        {
            return people;
        }

        // BUILIDNG BLOCKS
        abstract class Attendee
        {
            // Stuff all attendees have
        }

        class Child
        {
            // Your implementation here
            // Stuff a child would do/have
        }

        class Senior
        {
            // Your implementation here
            // Stuff a senior would do/have
        }

        // Rename this function to something that makes more sense!
        public void InjectAttendee()
        {
            // Your implementation here
        }
    }
}
