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
    
    public partial class UserStatistic
    {
        public int STATISTICS_ID { get; set; }
        public Nullable<int> USER_ID { get; set; }
        public Nullable<int> SCORE_NUMBER { get; set; }
        public Nullable<int> GAMES_NUMBER { get; set; }
    
        public virtual User User { get; set; }
    }
}