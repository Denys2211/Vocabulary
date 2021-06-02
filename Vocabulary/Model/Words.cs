using System;
using System.Collections.Generic;
using SQLite;

namespace Vocabulary.Model
{
    [Table("Words")]
    public class Words
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string EnglishWords { get; set; }
        public string UkrainianWords { get; set; }
        public string DateTime { get; set; }

        public static implicit operator string(Words v)
        {
            throw new NotImplementedException();
        }
    }
}
