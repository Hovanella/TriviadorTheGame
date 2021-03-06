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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Questions = new HashSet<Question>();
            this.QuestionsPacks = new HashSet<QuestionsPack>();
            this.UserStatistics = new HashSet<UserStatistic>();
        }

        public User(string login, string password, string role)
        {
            USER_ROLE=role;
            USER_LOGIN = login;
            USER_PASSWORD = password;
        }

        public int USER_ID { get; set; }
        public string USER_LOGIN { get; set; }
        public string USER_PASSWORD { get; set; }
        public string USER_ROLE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionsPack> QuestionsPacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserStatistic> UserStatistics { get; set; }
    }
}
