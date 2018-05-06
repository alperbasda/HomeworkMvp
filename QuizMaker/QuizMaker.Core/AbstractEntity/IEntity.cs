using System.ComponentModel.DataAnnotations;

namespace QuizMaker.Core.AbstractEntity
{
    public interface IEntity
    {
        [Key]
        int Id { get; set; }
    }
}