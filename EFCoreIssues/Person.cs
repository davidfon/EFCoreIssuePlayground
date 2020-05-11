using System;

namespace EFCoreIssues
{
    public class Person
    {
        public Guid Id { get; private set; }
        public PersonName PersonName { get; private set; }

        private Person() { }

        public Person(PersonName personName)
        {
            Id = Guid.NewGuid();
            PersonName = personName;
        }
    }
}
