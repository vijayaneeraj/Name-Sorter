namespace NameSorter.Model
{
    // Data Transfer Object (DTO) 
    public class NameModel 
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(FirstName)
                  ? string.Format("{0}", LastName)
                  : string.Format("{0}, {1}", LastName, FirstName);
        }

    }
}