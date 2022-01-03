namespace Journal.Domain
{
    public class User
    {
        public User(string name)
        {
            Name = name;
            CreationDateTime = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}