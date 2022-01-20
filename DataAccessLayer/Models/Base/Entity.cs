namespace DataAccessLayer.Models.Base
{
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
    }
}