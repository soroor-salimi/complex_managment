namespace ComplexManagment.Entities
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int BlockId { get; set; }
        public ResidentOfType ResidentOfType { get; set; }
    }
}
