using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuickSnippAPI.Models
{
    public class Snippet
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Create field to hold code, set it to type varchar(MAX) in DB
        [Column(TypeName = "varchar(MAX)")]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public Boolean isPublic { get; set; } // If snippet available to organizations

        // Constructor
        public Snippet()
        {
            // Set default model values
            isPublic = false;
        }
    }

    public class SnippetDBContext : DbContext
    {
        public DbSet<Snippet> Snippet { get; set; }

        // You need to add the SnippetDBContext connection string to the web.config, or it creates another database!
    }
}