using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace HealthCatalystWebApp.Models
{
    /// <summary>
    /// Model for storing Person information
    /// </summary>
    public class PeopleModel
    {
        //The Primary key identifier, this will also be used to save unique pictures for person
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

        //This is the file URL to use in the image control
        [Required, StringLength(500)]
        public string picture { get; set; }
    }

    /// <summary>
    /// Database Context to use in pulling Person data
    /// </summary>
    public class PeopleContext : DbContext
    {
        public DbSet<PeopleModel> People { get; set; }
    }
}
