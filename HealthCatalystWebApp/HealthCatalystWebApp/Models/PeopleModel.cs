using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace HealthCatalystWebApp.Models
{
    public class PeopleModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PID { get; set; }

        [Required, DataType(DataType.Text), StringLength(50)]
        public string firstName { get; set; }

        [Required, DataType(DataType.Text), StringLength(50)]
        public string lastName { get; set; }

        [Required, DataType(DataType.MultilineText), StringLength(500)]
        public string address { get; set; }

        [Required, DataType(DataType.Text), Range(1, 100)]
        public int age { get; set; }

        [Required, DataType(DataType.MultilineText), StringLength(500)]
        public string interests { get; set; }

        [Required, StringLength(500)]
        public string picture { get; set; }
    }

    public class PeopleContext : DbContext
    {
        public DbSet<PeopleModel> People { get; set; }
    }
}
