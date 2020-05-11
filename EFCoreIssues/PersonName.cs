
using System.Collections.Generic;

namespace EFCoreIssues
{
    public class PersonName
    {
        public string FirstName { get; private set; }
        public string MiddleInitial { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }

        private PersonName() { }

        public PersonName(string firstName, string middleInitial, string lastName, string fullName)
        {
            FirstName = firstName;
            MiddleInitial = middleInitial;
            LastName = lastName;
            FullName = fullName;
        }
    }
}
