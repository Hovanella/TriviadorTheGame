//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TriviadorTheGame.Models.DataBaseModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            this.QuestionsToPacks = new HashSet<QuestionsToPack>();
        }
    
        public int QUESTION_ID { get; set; }
        public string QUESTION_TEXT { get; set; }
        public string RIGHT_ANSWER { get; set; }
        public string FIRST_WRONG_ANSWER { get; set; }
        public string SECOND_WRONG_ANSWER { get; set; }
        public string THIRD_WRONG_ANSWER { get; set; }
        public Nullable<int> CREATOR_ID { get; set; }

        public override string ToString()
        {
           return (QUESTION_TEXT+RIGHT_ANSWER+FIRST_WRONG_ANSWER+SECOND_WRONG_ANSWER+THIRD_WRONG_ANSWER).ToLower();
        }

        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionsToPack> QuestionsToPacks { get; set; }
    }
}
