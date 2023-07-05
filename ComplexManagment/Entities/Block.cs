namespace ComplexManagment.Entities
{
    public class Block
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public int ComplexId { get; set; }
        public Complex Complex { get; set; }
    }
}
