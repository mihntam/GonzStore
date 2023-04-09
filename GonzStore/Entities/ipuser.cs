
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GonzStore.Entities
{
    [Table("IPuser")]
    public class IPuser
    {
        [Key]
        public int idIPuser { get; set; }
      
        public int idUser { get; set; }
        [ForeignKey("idUser")]
        public User User { get; set; }

        public string diachiip { get; set; }
    }
}
